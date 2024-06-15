using Microsoft.AspNetCore.Mvc;
using CryptoViewer.DAL.Models;
using CryptoViewer.BL.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CryptoViewer.MVC.Controllers
{
    public class TrackerController : Controller
    {
        private readonly CryptocurrencyRepository _repository;

        public TrackerController(CryptocurrencyRepository repository)
        {
            _repository = repository;
        }

        [Route("/trackers")]
        public async Task<IActionResult> Tracker()
        {
            IEnumerable<Cryptocurrency> cryptocurrencies = await _repository.GetCryptocurrenciesAsync();
            return View(cryptocurrencies);
        }
    }
}
