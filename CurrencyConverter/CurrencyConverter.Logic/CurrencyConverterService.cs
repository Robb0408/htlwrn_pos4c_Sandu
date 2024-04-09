using CurrencyConverter.Logic.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace CurrencyConverter.Logic
{
    public class CurrencyConverterService : ICurrencyConverterService
    {
        /// <summary>
        /// Converts the amount from one currency to another
        /// </summary>
        /// <param name="fromCurrency">Currency from where the amount comes</param>
        /// <param name="toCurrency">Target currency to calculate</param>
        /// <param name="amount">Amount to be converted</param>
        /// <returns>Converted amount in target currency</returns>
        public decimal ConvertFromTo(decimal fromCurrencyRate, decimal toCurrencyRate, decimal amount)
        {
            // Multiplication: TO Currency, Division: FROM Currency e.g. 30000 USD / 1.1 Rate = 27272.727273 EUR * 0.89 Rate = 24242.424242 GPB
            return decimal.Round(amount / fromCurrencyRate * toCurrencyRate, 2); 
        }

        /// <summary>
        /// Converts the amount to EUR
        /// </summary>
        /// <param name="currency">Currency from where the amount comes</param>
        /// <param name="amount">Amount to be converted</param>
        /// <returns>Converted amount in EUR</returns>
        public decimal ConvertToEur(decimal currencyRate, decimal amount)
        {
            return decimal.Round(amount / currencyRate, 2);
        }

        /// <summary>
        /// Puts the currencies into a dictionary
        /// </summary>
        /// <param name="content">CSV-Data</param>
        /// <returns>Dictionary with currency code as key and exchange rate as value</returns>
        public Dictionary<string, decimal> GetCurrencies(string content)
        {
            return PrepareLines(content).Select(parts =>
            {
                return new KeyValuePair<string, decimal>(parts[1], decimal.Parse(parts[2], NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture));
            }).ToDictionary(x => x.Key, x => x.Value);
        }

        /// <summary>
        /// Puts the products into a list
        /// </summary>
        /// <param name="content">CSV-Data</param>
        /// <returns>List of products</returns>
        public List<Product> GetProducts(string content)
        {
            return PrepareLines(content).Select(parts =>
            {
                return new Product
                {
                    Description = parts[0],
                    Currency = parts[1],
                    Price = decimal.Parse(parts[2], NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture)
                };
            }).ToList();
        }

        /// <summary>
        /// Helper function to split the CSV-Data string into lines and split the lines into parts
        /// </summary>
        /// <remarks>
        /// Also skips a line (headline) and empty lines
        /// </remarks>
        /// <param name="content"></param>
        /// <returns></returns>
        private IEnumerable<string[]> PrepareLines(string content)
        {
            return content.Split("\r\n").Where(line => line.Length > 0).Skip(1).Select(line => 
            {
                if (line.Contains(","))
                {
                    return line.Split(",");
                }
                else
                {
                    return line.Split(";");
                }
            });
        }
    }
}
