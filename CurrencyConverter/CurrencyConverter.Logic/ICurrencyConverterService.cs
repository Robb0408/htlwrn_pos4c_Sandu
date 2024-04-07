using System;
using System.Collections.Generic;
using System.Text;
using CurrencyConverter.Logic.Models;

namespace CurrencyConverter.Logic
{
    public interface ICurrencyConverterService
    {
        Dictionary<string, decimal> GetCurrencies(string content);
        List<Product> GetProducts(string content);
        decimal ConvertToEur(decimal currencyRate, decimal amount);
        decimal ConvertFromTo(decimal fromCurrencyRate, decimal toCurrencyRate, decimal amount);
    }
}
