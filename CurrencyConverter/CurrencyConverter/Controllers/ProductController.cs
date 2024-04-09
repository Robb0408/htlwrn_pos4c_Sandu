using CurrencyConverter.Logic.Models;
using CurrencyConverter.Logic;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace CurrencyConverter.Api.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private IHttpClientFactory clientFactory;
        private ICurrencyConverterService currencyConverterService;
        private Dictionary<string, decimal> exchangeRates = [];
        private List<Product> productList = [];

        public ProductController(IHttpClientFactory clientFactory, ICurrencyConverterService currencyConverterService)
        {
            this.clientFactory = clientFactory;
            this.currencyConverterService = currencyConverterService;

        }

        /// <summary>
        /// Get the price of a product in a target currency
        /// </summary>
        /// <param name="product">Name of product with given currency and price</param>
        /// <param name="targetCurrency">Target currency to calculate to</param>
        /// <response code="200">Price of the product in the target currency</response>
        /// <response code="404">Product or target currency not found</response>
        [HttpGet("{product}/price")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(decimal))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetPrice(string product, [FromQuery] string targetCurrency)
        {
            if (exchangeRates.Count == 0 || productList.Count == 0)
            {
                HttpClient client = clientFactory.CreateClient("ExchangeRates");
                var response = await client.GetStringAsync("currencies.csv");
                exchangeRates = currencyConverterService.GetCurrencies(response);
                exchangeRates.Add("EUR", 1);
                response = await client.GetStringAsync("products.csv");
                productList = currencyConverterService.GetProducts(response);
            }
            if (targetCurrency is null || !exchangeRates.ContainsKey(targetCurrency))
            {
                return NotFound("Target currency not found");
            }
            var productResult = productList.FirstOrDefault(p => p.Description == product);
            if (productResult is null)
            {
                return NotFound("Product not found");
            }
            if (targetCurrency == "EUR")
            {
                return Ok(new
                {
                    Price = currencyConverterService.ConvertToEur(exchangeRates[productResult.Currency], productResult.Price)
                });
            }
            return Ok(new
            {
                Price = currencyConverterService
                .ConvertFromTo(exchangeRates[productResult.Currency], exchangeRates[targetCurrency], productResult.Price)
            });
        }
    }
}
