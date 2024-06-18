using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using CryptoViewer.MVC.Models.Dto;

namespace CryptoViewer.MVC.Helpers
{
    /// <summary>
    /// Helper class to interact with APIs using HttpClient.
    /// </summary>
    public class ApiHelper : IApiHelper
    {
        private readonly HttpClient _httpClient;
        private const string BASE_URL = "http://localhost:5004/";

        private bool IsSettedJWT = false;

        /// <summary>
        /// Constructor to initialize the ApiHelper with an instance of HttpClient.
        /// </summary>
        /// <param name="httpClient">Instance of HttpClient to use for API requests.</param>
        public ApiHelper(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        /// <summary>
        /// Retrieves a bearer token from the API using the provided username and password.
        /// </summary>
        /// <param name="username">Username for authentication.</param>
        /// <param name="password">Password for authentication.</param>
        /// <returns>The bearer token as a string.</returns>
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

        /// <summary>
        /// Sets the Bearer token in the default request headers of HttpClient.
        /// </summary>
        private async Task SetBearerToken()
        {
            var token = await GetBearerToken("style", "style");

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            IsSettedJWT = true;
        }

        /// <summary>
        /// Sends an HTTP GET request to the specified URL and deserializes the JSON response to the specified type.
        /// </summary>
        /// <typeparam name="T">The type to deserialize the JSON response to.</typeparam>
        /// <param name="url">The URL of the API endpoint.</param>
        /// <returns>An object of type T representing the API response.</returns>
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

        /// <summary>
        /// Sends an HTTP POST request to the specified URL with the serialized data object and deserializes the JSON response to the specified type.
        /// </summary>
        /// <typeparam name="T">The type to deserialize the JSON response to.</typeparam>
        /// <param name="url">The URL of the API endpoint.</param>
        /// <param name="data">The data object to send as JSON in the request body.</param>
        /// <returns>An object of type T representing the API response.</returns>
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

        /// <summary>
        /// Sends an HTTP PUT request to the specified URL with the serialized data object and deserializes the JSON response to the specified type.
        /// </summary>
        /// <typeparam name="T">The type to deserialize the JSON response to.</typeparam>
        /// <param name="url">The URL of the API endpoint.</param>
        /// <param name="data">The data object to send as JSON in the request body.</param>
        /// <returns>An object of type T representing the API response.</returns>
        public async Task<T> PutAsync<T>(string url, object data)
        {
            if (!IsSettedJWT)
            {
                await SetBearerToken();
            }
            var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
            using (var response = await _httpClient.PutAsync(BASE_URL + url, content))
            {
                response.EnsureSuccessStatusCode();
                var responseData = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(responseData);
            }
        }

        /// <summary>
        /// Sends an HTTP DELETE request to the specified URL.
        /// </summary>
        /// <param name="url">The URL of the API endpoint.</param>
        /// <returns>Task representing the asynchronous operation.</returns>
        public async Task DeleteAsync(string url)
        {
            if (!IsSettedJWT)
            {
                await SetBearerToken();
            }
            var response = await _httpClient.DeleteAsync(BASE_URL + url);
            response.EnsureSuccessStatusCode();
        }
    }
}