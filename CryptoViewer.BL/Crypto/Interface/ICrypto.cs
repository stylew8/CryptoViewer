using CryptoViewer.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoViewer.BL.Crypto.Interface
{
    /// <summary>
    /// Interface for managing cryptocurrencies.
    /// </summary>
    public interface ICrypto
    {
        /// <summary>
        /// Retrieves all cryptocurrencies asynchronously.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains a collection of cryptocurrencies.</returns>
        Task<IEnumerable<Cryptocurrency>> GetCryptocurrenciesAsync();

        /// <summary>
        /// Adds a new cryptocurrency asynchronously.
        /// </summary>
        /// <param name="cryptocurrency">The cryptocurrency object to add.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task AddCryptocurrencyAsync(Cryptocurrency cryptocurrency);

        /// <summary>
        /// Updates an existing cryptocurrency asynchronously.
        /// </summary>
        /// <param name="cryptocurrency">The cryptocurrency object with updated information.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task UpdateCryptocurrencyAsync(Cryptocurrency cryptocurrency);

        /// <summary>
        /// Deletes a cryptocurrency asynchronously by its identifier.
        /// </summary>
        /// <param name="id">The identifier of the cryptocurrency to delete.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task DeleteCryptocurrencyAsync(int id);
    }
}