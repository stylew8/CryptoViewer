using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CryptoViewer.DAL.Crypto.Interfaces;
using CryptoViewer.DAL.Helpers;
using CryptoViewer.DAL.Models;


namespace CryptoViewer.DAL.Crypto
{
    
    public class CryptocurrencyDAL : ICryptocurrencyDAL
    {
        private readonly IDbHelper _dbHelper;

        public CryptocurrencyDAL(IDbHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        public async Task<IEnumerable<Cryptocurrency>> GetCryptocurrenciesAsync()
        {
            var sql = "SELECT Id, Name, LogoPath, TrackerAction, BorderColor FROM Cryptocurrencies";
            return await _dbHelper.QueryAsync<Cryptocurrency>(sql, null);
        }

        public async Task AddCryptocurrencyAsync(Cryptocurrency cryptocurrency)
        {
            var sql = "INSERT INTO Cryptocurrencies (Name, LogoPath, TrackerAction, BorderColor) VALUES (@Name, @LogoPath, @TrackerAction, @BorderColor)";
            await _dbHelper.ExecuteAsync(sql, cryptocurrency);
        }
        public async Task UpdateCryptocurrencyAsync(Cryptocurrency cryptocurrency)
        {
            var sql = "UPDATE Cryptocurrencies SET Name = @Name, LogoPath = @LogoPath, TrackerAction = @TrackerAction, BorderColor = @BorderColor WHERE Id = @Id";
            await _dbHelper.ExecuteAsync(sql, cryptocurrency);

        }

        public async Task DeleteCryptocurrencyAsync(int id)
        {
            var sql = "DELETE FROM Cryptocurrencies WHERE Id = @Id";
            await _dbHelper.ExecuteAsync(sql, new { Id = id });
        }

    }
}
