using CashRegister.Shared.Dto;
using CashRegister.Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace CashRegister.Api.Controllers
{
    [Route("api/[controller]s")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly CashRegisterContext context;

        public ProductController(CashRegisterContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] string? nameFilter = null)
        {
            IQueryable<Product> products = context.Products;

            if (!string.IsNullOrEmpty(nameFilter))
            {
                products = products.Where(p => p.ProductName.Contains(nameFilter));
            }

            return Ok(await products.ToListAsync());
        }

        [HttpPost("receipts")]
        public async Task<IActionResult> Post([FromBody] IEnumerable<ReceiptLineDto> receiptLineDtos)
        {
            if (receiptLineDtos.IsNullOrEmpty())
            {
                return BadRequest("There must be at least one receipt line");
            }
            // Read product data from DB for incoming product IDs
            var products = new Dictionary<int, Product>();

            // Here you have to add code that reads all products referenced by product IDs
            // in receiptDto.Lines and store them in the `products` dictionary.
            var productIDs = receiptLineDtos.Select(p => p.ProductID);
            var result = context.Products.Where(p => productIDs.Contains(p.ID));
            if (result.Count() != productIDs.Count())
            {
                return BadRequest("An provided ID does not match with an ID from database");
            }
            foreach (var product in result)
            {
                products.Add(product.ID, product);
            }

            // Build receipt from DTO
            var newReceipt = new Receipt
            {
                ReceiptTimestamp = DateTime.UtcNow,
                ReceiptLines = receiptLineDtos.Select(rl => new ReceiptLine
                {
                    ID = 0,
                    Product = products[rl.ProductID],
                    Amount = rl.Amount,
                    TotalPrice = rl.Amount * products[rl.ProductID].UnitPrice
                }).ToList()
            };
            newReceipt.TotalPrice = newReceipt.ReceiptLines.Sum(rl => rl.TotalPrice);
            context.Add(newReceipt);
            await context.SaveChangesAsync();
            return Ok($"Receipt saved");
        }
    }
}
