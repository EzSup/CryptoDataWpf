﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoDataWpf.Application.Interfaces.Services
{
    public interface ICurrencyCalculationsService
    {
        Task<decimal> ExchangeCurrencies(string firstCurrencyName, string secondCurrencyName);
    }
}
