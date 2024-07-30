using CryptoDataWpf.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoDataWpf.Application.Interfaces.Services
{
    public interface IOhlcService
    {
        Task<ICollection<FinancialData>> GetFinancialDatas(string symbol, string timePeriod);
    }
}
