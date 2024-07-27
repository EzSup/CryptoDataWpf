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

namespace CryptoDataWpf.Infrastructure.APIs
{
    public class CoinCapService : IAPIInteractionService
    {
        private readonly HttpClient _httpClient;

        public CoinCapService(IHttpClientFactory factory) 
        {
            _httpClient = factory.CreateClient("CoinCap");
        }

        public async Task<ICollection<Currency>> GetAssets(string? search, int countLimit = 10)
        {
            try
            {
                var response = await _httpClient.GetAsync($"/v2/assets?limit={countLimit}&search={search}");
                response.EnsureSuccessStatusCode();
                var responseBody = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<ICollection<Currency>>(responseBody);
                return result ?? new List<Currency>();
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
    }
}
