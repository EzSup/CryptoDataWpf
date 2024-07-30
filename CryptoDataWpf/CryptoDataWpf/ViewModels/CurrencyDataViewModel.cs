using CryptoDataWpf.Application.Interfaces.Services;
using System.Collections.ObjectModel;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using CryptoDataWpf.Core.Models;
using CryptoDataWpf.Core.CustomExceptions;
using System.Windows;

namespace CryptoDataWpf.ViewModels
{
    class CurrencyDataViewModel : INotifyPropertyChanged
    {
        public Currency _currency { get; set; }        
        private readonly IAPIInteractionService _apiService;

        public Currency Currency
        {
            get => _currency;
            set
            {
                _currency = value;
                OnPropertyChanged(nameof(Currency));
            }
        }

        public CurrencyDataViewModel(IAPIInteractionService apiService, string CurrencyCode)
        {
            _apiService = apiService;
            LoadCurrencyData(CurrencyCode);
        }



        private async Task LoadCurrencyData(string currencyCode)
        {
            try
            {
                Currency = await _apiService.GetAsset(currencyCode);
            }
            catch (CurrencyNotFoundException ex)
            {
                string errorMessage = string.Format("Currency searching exception: {0}", ex.Message);
                MessageBox.Show(errorMessage, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
