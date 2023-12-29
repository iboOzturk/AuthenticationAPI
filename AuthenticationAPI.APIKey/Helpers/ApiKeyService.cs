using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace AuthenticationAPI.APIKey.Helpers
{
    public interface IApiKeyService
    {
        Task<bool> IsApiKeyValidAsync(string apiKey);
    }
    public class ApiKeyService : IApiKeyService
    {
        private readonly HttpClient _httpClient;
        string userName = "ibo";
        string pass= "123";

        public ApiKeyService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:44318/api/Sample/");
        }
        public async Task<bool> IsApiKeyValidAsync(string apiKey)
        {
            var responseToken = await _httpClient.GetAsync($"GetToken?username={userName}&pass={pass}");
            
            if (responseToken.IsSuccessStatusCode)
            {
                var token= await responseToken.Content.ReadAsStringAsync();
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var response = await _httpClient.GetAsync($"SecretKey?keyValue={apiKey}");

                return response.IsSuccessStatusCode;
            }
            return false;
           
        }
    }
}
