using CryptoDataWpf.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoDataWpf.Application.Interfaces.Services
{
    public interface ICurrencyCalculationsService
    {
        decimal ExchangeCurrencies(Currency firstCurrency, Currency secondCurrency, decimal firstCurrencyAmount);
    }
}
