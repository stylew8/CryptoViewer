using CryptoViewer.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoViewer.BL.Repositories.Interfaces
{
    public interface ICryptocurrencyRepository
    {
        public Task<IEnumerable<Cryptocurrency>> GetCryptocurrenciesAsync();
        public Task AddCryptocurrencyAsync(Cryptocurrency cryptocurrency);
    }
}
