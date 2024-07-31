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
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore;
using LiveChartsCore.Defaults;

namespace CryptoDataWpf.ViewModels
{
    class CurrencyDataViewModel : INotifyPropertyChanged
    {
        private Currency _currency { get; set; }
        FinancialData[] _financialData { get; set; }
        private readonly ICurrencyDataService _apiService;
        private readonly IOhlcService _ohlcService;
        private Axis[] _xaxes;
        private ISeries[] _series;

        public Axis[] XAxes 
        { 
            get => _xaxes; 
            set
            {
                _xaxes = value;
                OnPropertyChanged(nameof(XAxes));
            }         
        }
        public ISeries[] Series 
        { 
            get => _series; 
            set 
            {
                _series = value;
                OnPropertyChanged(nameof(Series));
            } 
        }

        public FinancialData[] FinancialData
        {
            get => _financialData;
            set
            {
                _financialData = value;
                OnPropertyChanged(nameof(FinancialData));
            }
        }

        public Currency Currency
        {
            get => _currency;
            set
            {
                _currency = value;
                OnPropertyChanged(nameof(Currency));
            }
        }

        public CurrencyDataViewModel(ICurrencyDataService apiService, IOhlcService ohlcService, string CurrencyCode)
        {
            _apiService = apiService;
            _ohlcService = ohlcService;
            LoadCurrencyData(CurrencyCode);
        }

        private async void LoadCurrencyData(string currencyCode)
        {
            try
            {
                Currency = await _apiService.GetAsset(currencyCode);
                FinancialData = (await _ohlcService.GetFinancialDatas(currencyCode, "1hour"))?.ToArray() ?? Array.Empty<FinancialData>();

                Series = new ISeries[]
                {
                    new CandlesticksSeries<FinancialPointI>
                    {
                        Values = FinancialData?.Select(x => new FinancialPointI(x.High, x.Open, x.Close, x.Low))
                                          .ToArray() ?? Array.Empty<FinancialPointI>()
                    }
                };

                XAxes = new[]
                {
                    new Axis
                    {
                        LabelsRotation = 15,
                        Labels = FinancialData?.Select(x => x.Timestamp.ToString())
                                              .ToArray() ?? Array.Empty<string>()
                    }
                };
            }
            catch (CurrencyNotFoundException ex)
            {
                string errorMessage = string.Format("Currency searching exception: {0}. Chart will be empty!", ex.Message);
                MessageBox.Show(errorMessage, "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
