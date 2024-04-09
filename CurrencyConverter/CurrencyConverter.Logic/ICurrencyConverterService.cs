using System;
using System.Collections.Generic;
using System.Text;
using CurrencyConverter.Logic.Models;

namespace CurrencyConverter.Logic
{
    public interface ICurrencyConverterService
    {
        /// <summary>
        /// Puts the currencies into a dictionary
        /// </summary>
        /// <param name="content">CSV-Data</param>
        /// <returns>Dictionary with currency code as key and exchange rate as value</returns>
        Dictionary<string, decimal> GetCurrencies(string content);

        /// <summary>
        /// Puts the products into a list
        /// </summary>
        /// <param name="content">CSV-Data</param>
        /// <returns>List of products</returns>
        List<Product> GetProducts(string content);

        /// <summary>
        /// Converts the amount to EUR
        /// </summary>
        /// <param name="currency">Currency from where the amount comes</param>
        /// <param name="amount">Amount to be converted</param>
        /// <returns>Converted amount in EUR</returns>
        /// 
        decimal ConvertToEur(decimal currencyRate, decimal amount);
        /// <summary>
        /// Converts the amount from one currency to another
        /// </summary>
        /// <param name="fromCurrency">Currency from where the amount comes</param>
        /// <param name="toCurrency">Target currency to calculate</param>
        /// <param name="amount">Amount to be converted</param>
        /// <returns>Converted amount in target currency</returns>
        decimal ConvertFromTo(decimal fromCurrencyRate, decimal toCurrencyRate, decimal amount);
    }
}
