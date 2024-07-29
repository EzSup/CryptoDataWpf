using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoDataWpf.Core.CustomExceptions
{
    public class CurrencyNotFoundException : Exception
    {
        public CurrencyNotFoundException() { }

        public CurrencyNotFoundException(string name) : base($"Currency {name} was not found!") { }
    }
}
