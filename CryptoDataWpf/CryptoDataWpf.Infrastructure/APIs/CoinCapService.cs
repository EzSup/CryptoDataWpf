using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using CryptoDataWpf.Core.Models;
using CryptoDataWpf.Application.Interfaces.Services;
using CryptoDataWpf.Core.CustomExceptions;

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

        public async Task<ICollection<Currency>> GetAssets(string? search = null, int countLimit = 20)
        {
            try
            {
                var response = await _httpClient.GetAsync($"/v2/assets?limit={countLimit}&search={search}");
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
