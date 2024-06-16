namespace CryptoViewer.MVC.Helpers
{
    public interface IApiHelper
    {
        Task<T> GetAsync<T>(string url);
        Task<T> PostAsync<T>(string url, object data);
    }
}
