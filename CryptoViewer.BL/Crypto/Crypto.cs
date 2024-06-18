using CryptoViewer.DAL.Models;
using CryptoViewer.DAL.Helpers;
using System.Collections.Generic;
using System.Threading.Tasks;
using CryptoViewer.BL.Crypto.Interface;
using CryptoViewer.DAL.Crypto.Interfaces;

namespace CryptoViewer.DAL.Repositories
{
    /// <summary>
    /// Repository class for managing cryptocurrencies.
    /// </summary>
    public class Crypto : ICrypto
    {
        private readonly IDbHelper _dbHelper;
        private readonly ICryptocurrencyDAL crypto;

        /// <summary>
        /// Initializes a new instance of the <see cref="Crypto"/> class.
        /// </summary>
        /// <param name="dbHelper">The database helper instance.</param>
        /// <param name="crypto">The cryptocurrency data access layer.</param>
        public Crypto(IDbHelper dbHelper, ICryptocurrencyDAL crypto)
        {
            _dbHelper = dbHelper;
            this.crypto = crypto;
        }

        /// <summary>
        /// Retrieves all cryptocurrencies asynchronously.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains a collection of cryptocurrencies.</returns>
        public async Task<IEnumerable<Cryptocurrency>> GetCryptocurrenciesAsync()
        {
            return await crypto.GetCryptocurrenciesAsync();
        }

        /// <summary>
        /// Adds a new cryptocurrency asynchronously.
        /// </summary>
        /// <param name="cryptocurrency">The cryptocurrency object to add.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public async Task AddCryptocurrencyAsync(Cryptocurrency cryptocurrency)
        {
            await crypto.AddCryptocurrencyAsync(cryptocurrency);
        }

        /// <summary>
        /// Updates an existing cryptocurrency asynchronously.
        /// </summary>
        /// <param name="cryptocurrency">The cryptocurrency object with updated information.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public async Task UpdateCryptocurrencyAsync(Cryptocurrency cryptocurrency)
        {
            await crypto.UpdateCryptocurrencyAsync(cryptocurrency);
        }

        /// <summary>
        /// Deletes a cryptocurrency asynchronously by its identifier.
        /// </summary>
        /// <param name="id">The identifier of the cryptocurrency to delete.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public async Task DeleteCryptocurrencyAsync(int id)
        {
            await crypto.DeleteCryptocurrencyAsync(id);
        }
    }
}