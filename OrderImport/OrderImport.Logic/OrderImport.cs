using System.Globalization;
using Microsoft.EntityFrameworkCore;
using OrderImport.Database;

namespace OrderImport.Logic;

public class OrderImport
{
    /// <summary>
    /// Imports customer and order data from two given files into a database
    /// </summary>
    /// <param name="customerFile">The customer data</param>
    /// <param name="orderFile">The order data</param>
    /// <exception cref="FileNotFoundException"></exception>
    /// <exception cref="IOException"></exception>
    /// <exception cref="Exception"></exception>
    public async Task ImportAsync(string customerFile, string orderFile)
    {
        string[] customers;
        string[] orders;
        customers = await File.ReadAllLinesAsync(customerFile);
        orders = await File.ReadAllLinesAsync(orderFile);

        await using var context = new OrderImportContextFactory().CreateDbContext();
        var customerList = customers
            .Skip(1)
            .Select(customer =>
            {
                var splitCustomer = customer.Split("\t");
                return new Customer
                {
                    Name = splitCustomer[0],
                    CreditLimit = decimal.Parse(splitCustomer[1], CultureInfo.InvariantCulture),
                    Orders = new List<Order>()
                };
            })
            .ToList();

        context.Customers.AddRange(customerList);
        var rowsCustomers = await context.SaveChangesAsync();
        
        
        var orderList = orders
            .Skip(1)
            .Select(order =>
            {
                var splitOrder = order.Split("\t");
                var customer = context.Customers
                    .FirstOrDefault(c => c.Name == splitOrder[0]);
                return new Order
                {
                    OrderDate = DateTime.Parse(splitOrder[1], new CultureInfo("en-US")),
                    OrderValue = decimal.Parse(splitOrder[2], CultureInfo.InvariantCulture),
                    CustomerId = customer!.Id
                };
            })
            .ToList();
        
        context.Orders.AddRange(orderList);
        var rowsOrders = await context.SaveChangesAsync();
        Console.WriteLine("Imported {0} customers successfully", rowsCustomers);
        Console.WriteLine("Imported {0} orders successfully", rowsOrders);
    }
    
    /// <summary>
    /// Cleans all entries from the database
    /// </summary>
    public async Task CleanAsync()
    {
        await using var context = new OrderImportContextFactory().CreateDbContext();
        context.Customers.RemoveRange(context.Customers);
        context.Orders.RemoveRange(context.Orders);
        var rows = await context.SaveChangesAsync();
        Console.WriteLine("Cleaned {0} entries successfully", rows);
    }
    /// <summary>
    /// Shows a list of customers who have exceeded their credit limit 
    /// </summary>
    public async Task CheckAsync()
    {
        await using var context = new OrderImportContextFactory().CreateDbContext();
        var customers = await context.Customers
            .Include(c => c.Orders)
            .Where(c => c.CreditLimit < c.Orders.Sum(o => o.OrderValue))
            .ToListAsync();
        
        if (customers.Count == 0)
        {
            Console.WriteLine("No customers have exceeded their credit limit or " +
                              "there are no entries in the database");
            return;
        }
        var longestName = customers.Max(c => c.Name.Length) + 10;
        Console.WriteLine($"{"Name".PadRight(longestName)}" +
                          $"{"Credit Limit".PadRight(longestName)}" +
                          $"{"Total Order Value".PadRight(longestName)}" +
                          $"{"Exceeded by".PadRight(longestName)}");
        
        Console.ForegroundColor = ConsoleColor.Red;
        customers.ForEach(c =>
        {
            var total = c.Orders.Sum(o => o.OrderValue);
            Console.WriteLine($"{c.Name.PadRight(longestName)}" +
                              $"{c.CreditLimit.ToString("C").PadRight(longestName)}" +
                              $"{total.ToString("C").PadRight(longestName)}" +
                              $"{(c.CreditLimit - total).ToString("C").PadRight(longestName)}");
        });
        Console.WriteLine("\nFound {0} customers who have exceeded their credit limit", customers.Count);
        Console.ResetColor();
    }
    
    /// <summary>
    /// Shows a help text how to use the application
    /// </summary>
    public void ShowHelp()
    {
        Console.WriteLine("Usage: dotnet run -- <command> [<args>]");
        Console.WriteLine("  {0,-40} Imports customer and order data from two given files", 
            "import <customerFile> <orderFile>");
        Console.WriteLine("  {0,-40} Removes all customers and orders from the database", "clean");
        Console.WriteLine("  {0,-40} Shows a list of customers who have exceeded their credit limit", "check");
        Console.WriteLine("  {0,-40} Imports customer and order data from two given files, " +
                          "removes all customers and orders from the database and shows a list of customers who " +
                          "have exceeded their credit limit", "full <customerFile> <orderFile>");
    }
}