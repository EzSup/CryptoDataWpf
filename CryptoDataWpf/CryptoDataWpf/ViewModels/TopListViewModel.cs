using CryptoDataWpf.Application.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoDataWpf.ViewModels
{
    public class TopListViewModel
    {
        public ObservableCollection<CurrencyViewModel> Currencies { get; set; }
        private readonly IAPIInteractionService _apiService;

        public TopListViewModel(IAPIInteractionService apiService)
        {
            Currencies = new ObservableCollection<CurrencyViewModel>();
            _apiService = apiService;
        }

        public async Task InitializeAsync()
        {
            await RefreshCurrenciesList();
        }

        public async Task RefreshCurrenciesList()
        {
            var currencies = await _apiService.GetAssets();

            Currencies.Clear();

            foreach (var currency in currencies)
            {
                Currencies.Add(new CurrencyViewModel
                {
                    Name = currency.Name,
                    Symbol = currency.Symbol,
                    Rank = currency.Rank,
                    PriceUsd = currency.PriceUsd,
                    ChangePercent24Hr = currency.ChangePercent24Hr,
                    Vwap24Hr = currency.Vwap24Hr,
                    Explorer = currency.Explorer
                });
            }
        }        
    }
}
