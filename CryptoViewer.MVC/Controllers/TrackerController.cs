using Microsoft.AspNetCore.Mvc;
using CryptoViewer.DAL.Models;
using CryptoViewer.DAL.Repositories;
using CryptoViewer.MVC.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CryptoViewer.MVC.Controllers
{
    public class TrackerController : Controller
    {
        private readonly Crypto _repository;

        public TrackerController(Crypto repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [Route("/trackers")]
        public async Task<IActionResult> Tracker()
        {
            IEnumerable<Cryptocurrency> cryptocurrencies = await _repository.GetCryptocurrenciesAsync();
            return View(cryptocurrencies);
        }

        [HttpGet]
        [Route("/trackers/add")]
        [IgnoreAntiforgeryToken]
        public IActionResult AddCryptocurrency()
        {
            return View();
        }

        [HttpPost]
        [Route("/trackers/add")]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> AddCryptocurrency(AddCryptocurrencyViewModel model)
        {
            if (ModelState.IsValid)
            {
                var crypto = new Cryptocurrency
                {
                    Name = model.Name,
                    LogoPath = model.LogoPath,
                    TrackerAction = model.TrackerAction,
                    BorderColor = model.BorderColor
                };

                await _repository.AddCryptocurrencyAsync(crypto);
                return RedirectToAction("Tracker");
            }

            return View(model);
        }
    }
}
