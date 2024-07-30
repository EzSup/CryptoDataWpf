using CryptoDataWpf.Application.Interfaces.Services;
using CryptoDataWpf.Commands;
using CryptoDataWpf.Pages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;

namespace CryptoDataWpf.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private readonly Regex _currencyCodeRegex = new(@"^[a-zA-Z\s]+$");
        private readonly IAPIInteractionService _apiService;
        private readonly ICurrencyCalculationsService _calculationsService;
        private string _currencyCode = "";
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

        public MainWindowViewModel(IAPIInteractionService apiService, ICurrencyCalculationsService calculationsService)
        {
            _apiService = apiService;
            _calculationsService = calculationsService;

            SearchCurrencyCommand = new RelayCommand(SearchCurrency, CanSearchCurrency);
            SwitchPagesCommand = new RelayCommand(SwitchPage, CanSwitchPages);   
        }

        private bool CanSearchCurrency(object obj)
        {
            return true;
        }

        private bool CanSwitchPages(object obj)
        {
            return true;
        }

        private void SwitchPage(object obj)
        {
            var pageName = obj as string;
            switch (pageName)
            {
                case "Main":
                    TopList topListPage = new TopList(new(_apiService));
                    CurrentView = topListPage;
                    return;
                case "Exchange":
                    var viewModel = new ExchangesViewModel(_apiService, _calculationsService);
                    Exchanges exchangesPage =  new Exchanges(viewModel);
                    CurrentView = exchangesPage;
                    return;
                default:
                    SearchCurrency(pageName);                    
                    return;
            }            
        }

        private async void SearchCurrency(object obj)
        {
            var currencyDataViewModel = new CurrencyDataViewModel(_apiService, CurrencyCode);
            var currencyDetailView = new CurrencyData() { DataContext = currencyDataViewModel};
            CurrentView = currencyDetailView;
        }

        public ICommand SearchCurrencyCommand { get; set; }

        public ICommand SwitchPagesCommand { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            ((RelayCommand)SearchCurrencyCommand).RaiseCanExecuteChanged();
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
