using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using CryptoViewer.MVC.Models.Dto;

namespace CryptoViewer.MVC.Helpers
{
    public class ApiHelper : IApiHelper
    {
        private readonly HttpClient _httpClient;
        private const string BASE_URL = "https://localhost:7077/";

        private bool IsSettedJWT = false;

        public ApiHelper(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        private async Task<string> GetBearerToken(string username, string password)
        {
            var loginUrl = $"{BASE_URL}api/UsersAuth/login";
            var loginData = new
            {
                username = username,
                password = password
            };

            var content = new StringContent(JsonConvert.SerializeObject(loginData), Encoding.UTF8, "application/json");

            using (var response = await _httpClient.PostAsync(loginUrl, content))
            {
                response.EnsureSuccessStatusCode();
                var responseBody = await response.Content.ReadAsStringAsync();
                var tokenResponse = JsonConvert.DeserializeObject<LoginToApiDto>(responseBody);
                return tokenResponse.Result.token;
            }
        }

        private async Task SetBearerToken()
        {
            var token = await GetBearerToken("style","style");

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            IsSettedJWT = true;
        }

        public async Task<T> GetAsync<T>(string url)
        {
            if (!IsSettedJWT)
            {
               await SetBearerToken();
            }

            using (var response = await _httpClient.GetAsync(BASE_URL + url))
            {
                response.EnsureSuccessStatusCode();
                var data = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(data);
            }
        }

        public async Task<T> PostAsync<T>(string url, object data)
        {
            if (!IsSettedJWT)
            {
                await SetBearerToken();
            }

            var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
            using (var response = await _httpClient.PostAsync(BASE_URL + url, content))
            {
                var responseData = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(responseData);
            }
        }
    }
}
