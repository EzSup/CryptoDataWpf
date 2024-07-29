using CryptoDataWpf.Application.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoDataWpf.Application.Services
{
    public class CurrencyCalculationsService : ICurrencyCalculationsService
    {
        private readonly IAPIInteractionService _interactionService;

        public CurrencyCalculationsService(IAPIInteractionService interactionService)
        {
            _interactionService = interactionService;
        }

        public async Task<decimal> ExchangeCurrencies(string firstCurrencyName, string secondCurrencyName)
        {
            var firstCurrency = await _interactionService.GetAsset(firstCurrencyName);
            var secondCurrency = await _interactionService.GetAsset(secondCurrencyName);

            return firstCurrency.PriceUsd / secondCurrency.PriceUsd;
        }
    }
}
