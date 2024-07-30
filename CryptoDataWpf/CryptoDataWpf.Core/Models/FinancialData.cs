using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CryptoDataWpf.Core.Models
{
    public class FinancialData
    {        
        public long Timestamp { get; set; }
        public double Open { get; set; }
        public double High { get; set; }
        public double Low { get; set; }
        public double Close { get; set; }        
        public decimal Volume { get; set; }
        public decimal QuoteVolume { get; set; }

        public FinancialData(long timestamp, double open, double high, double low, double close, decimal volume, decimal quoteVolume)
        {
            Timestamp = timestamp;
            High = high;
            Open = open;
            Close = close;
            Low = low;
            Volume = volume;
            QuoteVolume = quoteVolume;
        }
    }
}
