using CryptoDataWpf.Application.Interfaces.Services;
using CryptoDataWpf.Commands;
using CryptoDataWpf.Core.Models;
using CryptoDataWpf.Pages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CryptoDataWpf.ViewModels
{
    public class ExchangesViewModel : INotifyPropertyChanged
    {
        private readonly IAPIInteractionService _apiService;
        private readonly ICurrencyCalculationsService _currencyCalculationsService;

        private Currency _targetCurrency = new Currency { Symbol = "NO", PriceUsd=0};
        private Currency _toBeExchangedCurrency = new Currency { Symbol = "NO", PriceUsd = 0 };
        private string _toBeExchangedCurrencyCode;
        private string _targetCurrencyCode;
        private decimal _toBeExchangedCurrencyAmount = 0;
        private decimal _targetCurrencyAmount = 0;

        public decimal TargetCurrencyAmount
        {
            get => _targetCurrencyAmount;
            set
            {
                _targetCurrencyAmount = value;
                OnPropertyChanged(nameof(TargetCurrencyAmount));
            }
        }

        public decimal ToBeExchangedCurrencyAmount
        {
            get => _toBeExchangedCurrencyAmount;
            set
            {
                _toBeExchangedCurrencyAmount = value;
                OnPropertyChanged(nameof(ToBeExchangedCurrencyAmount));
            }
        }

        public string ToBeExchangedCurrencyCode
        {
            get => _toBeExchangedCurrencyCode;
            set
            {
                _toBeExchangedCurrencyCode = value;
                OnPropertyChanged(nameof(ToBeExchangedCurrencyCode));
            }
        }

        public string TargetCurrencyCode
        {
            get => _targetCurrencyCode;
            set
            {
                _targetCurrencyCode = value;
                OnPropertyChanged(nameof(TargetCurrencyCode));
            }
        }

        public Currency TargetCurrency
        {
            get => _targetCurrency;
            set
            {
                _targetCurrency = value;
                OnPropertyChanged(nameof(TargetCurrency));
            }
        }

        public Currency ToBeExchangedCurrency
        {
            get => _toBeExchangedCurrency;
            set
            {
                _toBeExchangedCurrency = value;
                OnPropertyChanged(nameof(ToBeExchangedCurrency));
            }
        }

        public ExchangesViewModel(IAPIInteractionService apiService, ICurrencyCalculationsService currencyCalculationsService)
        {
            _apiService = apiService;
            _currencyCalculationsService = currencyCalculationsService;
            SetCurrencyCommand = new RelayCommand(SetCurrency, CanSetCurrency);
            ExchangeCommand = new RelayCommand(Exchange, CanExchange);
            SwapCurrenciesCommand = new RelayCommand(SwapCurrencies, CanSwapCurrencies);
        }

        private bool CanSetCurrency(object obj)
        {
            return true;
        }

        private bool CanExchange(object obj)
        {
            return true;
        }

        private bool CanSwapCurrencies(object obj)
        {
            return true;
        }

        private async void SetCurrency(object obj)
        {
            switch(obj as string)
            {
                case "targetCurrency":
                    TargetCurrency = await _apiService.GetAsset(TargetCurrencyCode);
                    break;
                case "toBeExchangedCurrency":
                    ToBeExchangedCurrency = await _apiService.GetAsset(ToBeExchangedCurrencyCode);
                    break;
            }
            
        }

        private void SwapCurrencies(object obj)
        {
            (TargetCurrency, ToBeExchangedCurrency) = (ToBeExchangedCurrency, TargetCurrency);
            (TargetCurrencyAmount, ToBeExchangedCurrencyAmount) = (ToBeExchangedCurrencyAmount, TargetCurrencyAmount);
            (TargetCurrencyCode, ToBeExchangedCurrencyCode) = (ToBeExchangedCurrencyCode, TargetCurrencyCode);
        }

        private void Exchange(object obj)
        {
            TargetCurrencyAmount = _currencyCalculationsService.ExchangeCurrencies(ToBeExchangedCurrency, TargetCurrency, _toBeExchangedCurrencyAmount);
        }

        public ICommand SetCurrencyCommand { get; set; }
        public ICommand ExchangeCommand { get; set; }
        public ICommand SwapCurrenciesCommand { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
