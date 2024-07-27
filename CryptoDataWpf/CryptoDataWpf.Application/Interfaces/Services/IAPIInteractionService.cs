using CryptoDataWpf.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoDataWpf.Application.Interfaces.Services
{
    public interface IAPIInteractionService
    {
        Task<ICollection<Currency>> GetAssets(string? search, int countLimit = 10);
    }
}
