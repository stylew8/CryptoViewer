using CryptoViewer.DAL.Models;
using CryptoViewer.DAL.Helpers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CryptoViewer.BL.Repositories
{
    public class CryptocurrencyRepository
    {
        private readonly IDbHelper _dbHelper;

        public CryptocurrencyRepository(IDbHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        public async Task<IEnumerable<Cryptocurrency>> GetCryptocurrenciesAsync()
        {
            var sql = "SELECT Id, Name, LogoPath, TrackerAction, BorderColor FROM Cryptocurrencies";
            return await _dbHelper.QueryAsync<Cryptocurrency>(sql, null);
        }
    }
}
