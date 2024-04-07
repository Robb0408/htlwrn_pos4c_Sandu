using CurrencyConverter.Logic.Models;
using CurrencyConverter.Logic;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet("{product}/price")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(decimal))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetPrice(string product, [FromQuery] string targetCurrency)
        {
            if (exchangeRates.Count == 0 || productList.Count == 0)
            {
                HttpClient client = clientFactory.CreateClient("ExchangeRates");
                var response = await client.GetStringAsync("currencies.csv");
                exchangeRates = currencyConverterService.GetCurrencies(response);
                response = await client.GetStringAsync("products.csv");
                productList = currencyConverterService.GetProducts(response);
            }
            if (targetCurrency is null || !(exchangeRates.ContainsKey(targetCurrency) || targetCurrency == "EUR"))
            {
                return BadRequest("Invalid target currency");
            }
            var productResult = productList.FirstOrDefault(p => p.Description == product);
            if (productResult is null)
            {
                return BadRequest("Invalid product");
            }
            if (targetCurrency == "EUR")
            {
                return Ok(currencyConverterService.ConvertToEur(exchangeRates[productResult.Currency], productResult.Price));
            }
            return Ok(currencyConverterService.ConvertFromTo(exchangeRates[productResult.Currency], exchangeRates[targetCurrency], productResult.Price));
        }
    }
}
