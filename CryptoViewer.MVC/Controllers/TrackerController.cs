using Microsoft.AspNetCore.Mvc;
using CryptoViewer.MVC.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using CryptoViewer.DAL.Models;
using CryptoViewer.MVC.Helpers;

namespace CryptoViewer.MVC.Controllers
{
    public class TrackerController : Controller
    {
        private readonly IApiHelper _apiHelper;

        public TrackerController(IApiHelper apiHelper)
        {
            _apiHelper = apiHelper;
        }

        [HttpGet]
        [Route("/trackers")]
        public async Task<IActionResult> Tracker()
        {
            try
            {
                var cryptocurrencies = await _apiHelper.GetAsync<IEnumerable<Cryptocurrency>>("api/TrackerApi");
                return View(cryptocurrencies);
            }
            catch (HttpRequestException ex)
            {
                
                return View("Error");
            }
        }

        [HttpGet]
        [Route("/trackers/add")]
       
        public IActionResult AddCryptocurrency()
        {
            return View();
        }

        [HttpPost]
        [Route("/trackers/add")]
       
        public async Task<IActionResult> AddCryptocurrency(AddCryptocurrencyViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _apiHelper.PostAsync<AddCryptocurrencyViewModel>("api/TrackerApi", model);
                return RedirectToAction("Tracker");
            }

            return View(model);
        }

        [HttpGet]
        [Route("/trackers/update")]
        
        public async Task<IActionResult> UpdateCryptocurrency()
        {
            return View();
        }

        [HttpPost]
        [Route("/trackers/update")]
        
        public async Task<IActionResult> UpdateCryptocurrency(UpdateCryptocurrencyViewModel model)
        {
            if (ModelState.IsValid)
            {
                var id = model.Id; 
                var result = await _apiHelper.PutAsync<AddCryptocurrencyViewModel>($"api/TrackerApi/{id}", model);
                return RedirectToAction("Tracker");
            }

            return View(model);
        }
    }
}
