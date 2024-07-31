using CryptoDataWpf.Core.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CryptoDataWpf.Application.Interfaces.Services;
using System.Globalization;
using CryptoDataWpf.Core.CustomExceptions;

namespace CryptoDataWpf.Infrastructure.APIs
{
    public class KuCoinService : IOhlcService
    {
        private readonly HttpClient _httpClient;

        public KuCoinService(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient("KuCoin");
        }

        public class ApiResponse
        {
            public long Code { get; set; }
            public List<List<string>> Data { get; set; }
            
        }

        public async Task<ICollection<FinancialData>> GetFinancialDatas(string symbol, string timePeriod)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/v1/market/candles?type={timePeriod}&startAt=1566703297&endAt=1566789757&symbol={symbol.ToUpper()}-USDT");
                response.EnsureSuccessStatusCode();
                var responseBody = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<ApiResponse>(responseBody);
                var parsed =  result.Data.Select(data => new FinancialData(
                                timestamp: long.Parse(data[0]),
                                open: double.Parse(data[1], CultureInfo.InvariantCulture),
                                high: double.Parse(data[2], CultureInfo.InvariantCulture),
                                low: double.Parse(data[3], CultureInfo.InvariantCulture),
                                close: double.Parse(data[4], CultureInfo.InvariantCulture),
                                volume: decimal.Parse(data[5], CultureInfo.InvariantCulture),
                                quoteVolume: decimal.Parse(data[6], CultureInfo.InvariantCulture)
                                ))
                                .ToList() ?? new List<FinancialData>();
                if(parsed.Count == 0)
                {
                    throw new ArgumentNullException();
                }
                return parsed;
            }
            catch (ArgumentNullException ex)
            {
                throw new CurrencyNotFoundException(symbol);
            }
            //catch (Exception ex)
            //{
            //    Console.Error.WriteLine($"Unexpected error: {ex.Message}");
            //    return new List<FinancialData>();
            //}
        }
    }
}
