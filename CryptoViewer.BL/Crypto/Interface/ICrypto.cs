using CryptoViewer.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoViewer.BL.Crypto.Interface
{
    public interface ICrypto
    {
        Task<IEnumerable<Cryptocurrency>> GetCryptocurrenciesAsync();
        Task AddCryptocurrencyAsync(Cryptocurrency cryptocurrency);
        Task UpdateCryptocurrencyAsync(Cryptocurrency cryptocurrency);
    }
}