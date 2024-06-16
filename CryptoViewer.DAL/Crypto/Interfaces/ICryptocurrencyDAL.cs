using CryptoViewer.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoViewer.DAL.Crypto.Interfaces
{
    public interface ICryptocurrencyDAL
    {
        Task<IEnumerable<Cryptocurrency>> GetCryptocurrenciesAsync();
        Task AddCryptocurrencyAsync(Cryptocurrency cryptocurrency);
        Task UpdateCryptocurrencyAsync(Cryptocurrency cryptocurrency);
    }
}
