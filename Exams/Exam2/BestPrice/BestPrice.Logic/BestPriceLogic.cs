using BestPrice.Data;
using BestPrice.Data.Model;
using BestPrice.Logic.ImportModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BestPrice.Logic
{
    // HAU: ℹ️ Split BestPriceLogic in ImportService, PriceCalculationService - Hint: Single Responsibility Principle
    public class BestPriceLogic
    {
        public async Task<bool> ImportDataFromFileAsync(string fileName)
        {
            // HAU: ℹ️ try catch for file read ad deserialize would be good
            var text = await File.ReadAllTextAsync(fileName);
            var jsonModel = JsonSerializer.Deserialize<BestPriceImportModel>(text)!;

            // HAU: ℹ️ you could use await using here - see remarks on grading for explanation why
            using var context = new BestPriceContextFactory().CreateDbContext();
        
            var products = jsonModel.Products.ToList();
            var availab = jsonModel.Availabilities.ToList();
            var specialoffers = jsonModel.SpecialOffers.ToList();
            var vendors = jsonModel.Vendors.ToList();

            // HAU: ❌ use BeginTransactionAsync 
            using var transaction = context.Database.BeginTransaction();
            try
            {

                foreach (var item in products)
                {
                    context.Products.Add(
                        new Product
                        {
                            Name = item
                        });

                    // HAU: ⚠️ save changes should be outside of the foreach loop to avoid to much database round-trips
                    await context.SaveChangesAsync();
                }

                foreach (var item in vendors)
                {
                    context.Vendors.Add(
                        new Vendor
                        {
                            Name = item
                        });

                    // HAU: ⚠️ save changes should be outside of the foreach loop to avoid to much database round-trips
                    await context.SaveChangesAsync();
                }

                foreach (var item in availab)
                {
                    var vendorId = context.Vendors.First(v => item.Vendor == v.Name);
                    var productId = context.Products.First(p => item.Product == p.Name);

                    context.Availability.Add(
                        new Availability
                        {
                            StockAmount = item.Amount,
                            Price = Convert.ToDecimal(item.Price),
                            VendorId = vendorId.VendorId,
                            ProductId = productId.ProductId
                        });

                    // HAU: ⚠️ save changes should be outside of the foreach loop to avoid to much database round-trips
                    await context.SaveChangesAsync();
                }

                foreach (var item in specialoffers)
                {
                    var vendorId = context.Vendors.First(v => item.Vendor == v.Name);
                    var productId = context.Products.First(p => item.Product == p.Name);

                    context.SpecialOffer.Add(
                        new SpecialOffer
                        {
                            DiscountRate = item.DiscountRate,
                            MinAmount = item.MinAmount,
                            ProductId = productId.ProductId,
                            VendorId = vendorId.VendorId
                        });

                    // HAU: ⚠️ save changes should be outside of the foreach loop to avoid to much database round-trips
                    await context.SaveChangesAsync();
                }
                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                return false;
            }
            return true;
        }

        public async Task DeleteAllAsync()
        {
            // HAU: ℹ️ you could use await using here - see remarks on grading for explanation why
            using var context = new BestPriceContextFactory().CreateDbContext();
            await context.Database.ExecuteSqlAsync($"DELETE FROM Availability");
            await context.Database.ExecuteSqlAsync($"DELETE FROM SpecialOffer");
            await context.Database.ExecuteSqlAsync($"DELETE FROM Product");
            await context.Database.ExecuteSqlAsync($"DELETE FROM Vendor");
            await context.SaveChangesAsync();
        }

        public async Task<string> CalculatePriceWithoutDiscountAsync(string fileName)
        {
            var shopping = await File.ReadAllLinesAsync(fileName);
            shopping = shopping[1..];
            var sb = new StringBuilder("Your shopping list:\n");

            // HAU: ℹ️ you could use await using here - see remarks on grading for explanation why
            using var context = new BestPriceContextFactory().CreateDbContext();
            var temp = new List<ProductPriceCalculationItem>();
            foreach (var item in shopping)
            {
                var split = item.Split(';');
                var product = split[0];
                
                // HAU: ❌ error resistant parsing of shopping list and fallback to amount 1 missing
                var amount = int.Parse(split[1]);

                /*var productFromDb = await context.Availability
                    .Include(x => x.Product)
                    .Where(a => a.StockAmount >= amount && a.Product.Name == product)
                    .OrderBy(a => a.Price)
                    .AsNoTracking()
                    .FirstAsync();*/

                // HAU: ⚠️ use FirstOrDefault otherwise you get an error when the product can't be found
                var productFromDb = await context.Products
                    // HAU: ℹ️ ThenInclude Vendor
                    .Include(p => p.Availabilities)
                    .Where(p => p.Name == product && p.Availabilities
                        .Where(a => a.ProductId == p.ProductId)
                        .Select(a => a.StockAmount)
                        .First() >= amount)
                    .AsNoTracking()
                    .FirstAsync();

                temp.Add(new ProductPriceCalculationItem(productFromDb, amount, new DefaultPriceStrategy()));
            }
            var sum = 0M;
            foreach (var item in temp)
            {
                sum += item.Price;

                /*var vendor = await context.Vendors
                    .Include(v => v.Availabilities)
                    .Where(v => v.Availabilities.Where(a => a.ProductId == item.Product.ProductId))
                    .AsNoTracking()
                    .FirstAsync();*/

                sb.Append(item.ToString());

            }
            sb.Append("---------\nTotal: " + sum);

            // HAU: ℹ️ you could also return the list of ProductPriceCalculationItem here
            //         or an custom created result object with an List of ProductPriceCalculationItem
            //         and TotalSum Property
            return sb.ToString();
        }
    }
}
