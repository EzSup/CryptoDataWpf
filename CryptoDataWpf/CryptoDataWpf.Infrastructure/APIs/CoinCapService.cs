using CryptoDataWpf.Application.Interfaces.Services;
using CryptoDataWpf.Core.CustomExceptions;
using CryptoDataWpf.Core.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CryptoDataWpf.Infrastructure.APIs
{
    public class CoinCapService : ICurrencyDataService
    {
        private readonly HttpClient _httpClient;

        public CoinCapService(IHttpClientFactory factory) 
        {
            _httpClient = factory.CreateClient("CoinCap");
        }

        public class ApiResponse
        {
            public List<Currency> Data { get; set; }
            public long Timestamp { get; set; }
        }

        public async Task<ICollection<Currency>> GetAssets(string? search = null, int countLimit = 60, int offset = 0)
        {
            try
            {
                var response = await _httpClient.GetAsync($"/v3/assets?limit={countLimit}&search={search}&offset={offset}");
                response.EnsureSuccessStatusCode();
                var responseBody = await response.Content.ReadAsStringAsync();

                var result = JsonConvert.DeserializeObject<ApiResponse>(responseBody);
                return result.Data ?? new List<Currency>();
            }
            catch (HttpRequestException ex)
            {
                Console.Error.WriteLine($"Request error: {ex.Message}");
                return new List<Currency>();
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Unexpected error: {ex.Message}");
                return new List<Currency>();
            }
        }   
        

        public async Task<Currency> GetAsset(string search)
        {
            var asset =  (await GetAssets(search, 1)).FirstOrDefault();
            if (asset is null)
                throw new CurrencyNotFoundException(search);
            return asset;
        }
    }
}
