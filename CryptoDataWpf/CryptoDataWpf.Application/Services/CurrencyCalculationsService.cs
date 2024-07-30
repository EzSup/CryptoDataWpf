using CryptoDataWpf.Application.Interfaces.Services;
using CryptoDataWpf.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoDataWpf.Application.Services
{
    public class CurrencyCalculationsService : ICurrencyCalculationsService
    {

        public decimal ExchangeCurrencies(Currency firstCurrency, Currency secondCurrency)
        {
            return firstCurrency.PriceUsd / secondCurrency.PriceUsd;
        }
    }
}
