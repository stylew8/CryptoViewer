using Microsoft.AspNetCore.Mvc;
using CryptoViewer.MVC.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using CryptoViewer.DAL.Models;

namespace CryptoViewer.MVC.Controllers
{
    public class TrackerController : Controller
    {
        private readonly HttpClient _httpClient;

        public TrackerController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [HttpGet]
        [Route("/trackers")]
        public async Task<IActionResult> Tracker()
        {
            var response = await _httpClient.GetAsync("http://localhost:5004/api/trackers");
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            var cryptocurrencies = JsonConvert.DeserializeObject<IEnumerable<Cryptocurrency>>(json);
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
                var content = new StringContent(JsonConvert.SerializeObject(model), System.Text.Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("http://localhost:5004/api/trackers", content);
                response.EnsureSuccessStatusCode();
                return RedirectToAction("Tracker");
            }

            return View(model);
        }
    }
}
