using System.Threading.Tasks;

namespace CryptoViewer.MVC.Helpers
{
    /// <summary>
    /// Interface for making HTTP requests to APIs.
    /// </summary>
    public interface IApiHelper
    {
        /// <summary>
        /// Sends an HTTP GET request to the specified URL and deserializes the JSON response to the specified type.
        /// </summary>
        /// <typeparam name="T">The type to deserialize the JSON response to.</typeparam>
        /// <param name="url">The URL of the API endpoint.</param>
        /// <returns>An object of type T representing the API response.</returns>
        Task<T> GetAsync<T>(string url);

        /// <summary>
        /// Sends an HTTP POST request to the specified URL with the serialized data object and deserializes the JSON response to the specified type.
        /// </summary>
        /// <typeparam name="T">The type to deserialize the JSON response to.</typeparam>
        /// <param name="url">The URL of the API endpoint.</param>
        /// <param name="data">The data object to send as JSON in the request body.</param>
        /// <returns>An object of type T representing the API response.</returns>
        Task<T> PostAsync<T>(string url, object data);

        /// <summary>
        /// Sends an HTTP PUT request to the specified URL with the serialized data object and deserializes the JSON response to the specified type.
        /// </summary>
        /// <typeparam name="T">The type to deserialize the JSON response to.</typeparam>
        /// <param name="url">The URL of the API endpoint.</param>
        /// <param name="data">The data object to send as JSON in the request body.</param>
        /// <returns>An object of type T representing the API response.</returns>
        Task<T> PutAsync<T>(string url, object data);

        /// <summary>
        /// Sends an HTTP DELETE request to the specified URL.
        /// </summary>
        /// <param name="url">The URL of the API endpoint.</param>
        /// <returns>Task representing the asynchronous operation.</returns>
        Task DeleteAsync(string url);
    }
}