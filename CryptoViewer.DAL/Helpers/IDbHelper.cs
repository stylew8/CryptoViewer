namespace CryptoViewer.DAL.Helpers
{
    public interface IDbHelper
    {
        Task<T> QueryScalarAsync<T>(string sql, object model);
        Task ExecuteAsync(string sql, object model);
        Task<IEnumerable<T>> QueryAsync<T>(string sql, object model);
    }
}
