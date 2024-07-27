using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoDataWpf.ViewModels
{
    public class CurrencyViewModel : INotifyPropertyChanged
    {
        private string? _name;
        private string? _symbol;
        private int _rank;
        private decimal _priceUsd;
        private decimal _changePercent24Hr;
        private decimal _vwap24Hr;
        private string? _explorer;

        public string? Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged(nameof(Name)); }
        }
        public string? Symbol
        {
            get { return _symbol; }
            set { _symbol = value; OnPropertyChanged(nameof(Symbol)); }
        }
        public int Rank
        {
            get { return _rank; }
            set { _rank = value; OnPropertyChanged(nameof(Rank)); }
        }
        public decimal PriceUsd
        {
            get { return Math.Round(_priceUsd, 2); }
            set { _priceUsd = value; OnPropertyChanged(nameof(PriceUsd)); }
        }
        public decimal ChangePercent24Hr
        {
            get { return Math.Round(_changePercent24Hr, 2); }
            set { _changePercent24Hr = value; OnPropertyChanged(nameof(ChangePercent24Hr)); }
        }
        public decimal Vwap24Hr
        {
            get { return Math.Round(_vwap24Hr,2); }
            set { _vwap24Hr = value; OnPropertyChanged(nameof(Vwap24Hr)); }
        }
        public string? Explorer
        {
            get { return _explorer; }
            set { _explorer = value; OnPropertyChanged(nameof(Explorer)); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
