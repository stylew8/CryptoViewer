using Dapper;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CryptoViewer.DAL.Helpers
{
    public class DbHelper : IDbHelper
    {
        public static string ConnString = ""; // Ensure this is properly initialized with your MySQL connection string

        public async Task ExecuteAsync(string sql, object model)
        {
            using (var connection = new MySqlConnection(ConnString))
            {
                await connection.OpenAsync();
                await connection.ExecuteAsync(sql, model);
            }
        }

        public async Task<T> QueryScalarAsync<T>(string sql, object model)
        {
            using (var connection = new MySqlConnection(ConnString))
            {
                await connection.OpenAsync();
                return await connection.QueryFirstOrDefaultAsync<T>(sql, model);
            }
        }

        public async Task<IEnumerable<T>> QueryAsync<T>(string sql, object model)
        {
            using (var connection = new MySqlConnection(ConnString))
            {
                await connection.OpenAsync();
                return await connection.QueryAsync<T>(sql, model);
            }
        }
    }
}