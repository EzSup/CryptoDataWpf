using CryptoDataWpf.Application.Interfaces.Services;
using CryptoDataWpf.Commands;
using CryptoDataWpf.Pages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Navigation;

namespace CryptoDataWpf.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private readonly IAPIInteractionService _apiService;
        private string _currencyCode;
        private object _currentView;

        public object CurrentView
        {
            get => _currentView;
            set
            {
                _currentView = value;
                OnPropertyChanged(nameof(CurrentView));
            }
        }

        public string CurrencyCode
        {
            get => _currencyCode;
            set
            {
                _currencyCode = value;
                OnPropertyChanged(nameof(CurrencyCode));
            }
        }

        public MainWindowViewModel(IAPIInteractionService apiService)
        {
            _apiService = apiService;

            SearchCurrencyCommand = new RelayCommand(SearchCurrency, CanSearchCurrency);
        }

        private bool CanSearchCurrency(object obj)
        {
            return true;
        }

        private async void SearchCurrency(object obj)
        {
            var currencyDataViewModel = new CurrencyDataViewModel(_apiService, CurrencyCode);
            var currencyDetailView = new CurrencyData() { DataContext = currencyDataViewModel};
            CurrentView = currencyDetailView;
        }

        public ICommand SearchCurrencyCommand { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
