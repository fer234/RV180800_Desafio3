using System.Net.Http.Json;
using System.Net.Http.Headers;
using Bolsa.Entities; 

namespace Bolsa.MVC.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;

        public ApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Plaza>> GetPlazasAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<List<Plaza>>("plazas");
            return response ?? new List<Plaza>();
        }

        public async Task<bool> PostularseAsync(int plazaId, string token)
        {
            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            var response = await _httpClient.PostAsJsonAsync("postulaciones", new { IdPlaza = plazaId });
            return response.IsSuccessStatusCode;
        }
    }
}