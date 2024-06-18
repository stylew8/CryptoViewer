using CryptoViewer.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoViewer.BL.Repositories.Interfaces
{
    /// <summary>
    /// Interface for accessing cryptocurrency data.
    /// </summary>
    public interface ICryptocurrencyRepository
    {
        /// <summary>
        /// Retrieves all cryptocurrencies asynchronously.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains an enumerable collection of <see cref="Cryptocurrency"/>.</returns>
        Task<IEnumerable<Cryptocurrency>> GetCryptocurrenciesAsync();

        /// <summary>
        /// Adds a new cryptocurrency asynchronously.
        /// </summary>
        /// <param name="cryptocurrency">The cryptocurrency object to add.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task AddCryptocurrencyAsync(Cryptocurrency cryptocurrency);
    }
}