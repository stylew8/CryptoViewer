using CryptoViewer.DAL.Models;
using CryptoViewer.DAL.Helpers;
using System.Collections.Generic;
using System.Threading.Tasks;
using CryptoViewer.BL.Crypto.Interface;
using CryptoViewer.DAL.Crypto.Interfaces;

namespace CryptoViewer.DAL.Repositories
{
    public class Crypto : ICrypto
    {
        private readonly IDbHelper _dbHelper;
        private readonly ICryptocurrencyDAL crypto;
        private readonly List<Cryptocurrency> _cryptocurrencies;
       


        public Crypto(IDbHelper dbHelper, ICryptocurrencyDAL crypto)
        {
            _dbHelper = dbHelper;
            this.crypto = crypto;
            _cryptocurrencies = new List<Cryptocurrency>();
        }

        public async Task<IEnumerable<Cryptocurrency>> GetCryptocurrenciesAsync()
        {
            return await crypto.GetCryptocurrenciesAsync();
        }

        public async Task AddCryptocurrencyAsync(Cryptocurrency cryptocurrency)
        {
            await crypto.AddCryptocurrencyAsync(cryptocurrency);
        }
        public async Task UpdateCryptocurrencyAsync(Cryptocurrency cryptocurrency)
        {
            await crypto.UpdateCryptocurrencyAsync(cryptocurrency);
        }
    }
}