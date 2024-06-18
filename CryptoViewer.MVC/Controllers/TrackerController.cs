using Microsoft.AspNetCore.Mvc;
using CryptoViewer.MVC.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using CryptoViewer.DAL.Models;
using CryptoViewer.MVC.Helpers;

namespace CryptoViewer.MVC.Controllers
{
    /// <summary>
    /// Controller responsible for managing cryptocurrency trackers.
    /// </summary>
    public class TrackerController : Controller
    {
        private readonly IApiHelper _apiHelper;

        /// <summary>
        /// Constructor to initialize the TrackerController with the API helper.
        /// </summary>
        /// <param name="apiHelper">The API helper instance for making API calls.</param>
        public TrackerController(IApiHelper apiHelper)
        {
            _apiHelper = apiHelper;
        }

        /// <summary>
        /// Action method for displaying a list of tracked cryptocurrencies.
        /// </summary>
        /// <returns>The view displaying the list of cryptocurrencies.</returns>
        [HttpGet]
        [Route("/trackers")]
        public async Task<IActionResult> Tracker()
        {
            try
            {
                var cryptocurrencies = await _apiHelper.GetAsync<IEnumerable<Cryptocurrency>>("api/TrackerApi");
                return View(cryptocurrencies);
            }
            catch (HttpRequestException)
            {
                return View("Error");
            }
        }

        /// <summary>
        /// Action method for displaying the form to add a new cryptocurrency to track.
        /// </summary>
        /// <returns>The view for adding a new cryptocurrency.</returns>
        [HttpGet]
        [Route("/trackers/add")]
        public IActionResult AddCryptocurrency()
        {
            return View();
        }

        /// <summary>
        /// Action method for handling the form submission to add a new cryptocurrency.
        /// </summary>
        /// <param name="model">The view model containing the cryptocurrency details.</param>
        /// <returns>Redirects to the Tracker action after adding the cryptocurrency.</returns>
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

        /// <summary>
        /// Action method for displaying the form to update a cryptocurrency's details.
        /// </summary>
        /// <returns>The view for updating a cryptocurrency.</returns>
        [HttpGet]
        [Route("/trackers/update")]
        public IActionResult UpdateCryptocurrency()
        {
            return View();
        }

        /// <summary>
        /// Action method for handling the form submission to update a cryptocurrency's details.
        /// </summary>
        /// <param name="model">The view model containing the updated cryptocurrency details.</param>
        /// <returns>Redirects to the Tracker action after updating the cryptocurrency.</returns>
        [HttpPost]
        [Route("/trackers/update")]
        public async Task<IActionResult> UpdateCryptocurrency(UpdateCryptocurrencyViewModel model)
        {
            if (ModelState.IsValid)
            {
                var id = model.Id;
                var result = await _apiHelper.PutAsync<UpdateCryptocurrencyViewModel>($"api/TrackerApi/{id}", model);
                return RedirectToAction("Tracker");
            }

            return View(model);
        }

        /// <summary>
        /// Action method for displaying the form to delete a cryptocurrency.
        /// </summary>
        /// <returns>The view for deleting a cryptocurrency.</returns>
        [HttpGet]
        [Route("/trackers/delete")]
        public IActionResult DeleteCryptocurrency()
        {
            return View();
        }

        /// <summary>
        /// Action method for handling the deletion of a cryptocurrency.
        /// </summary>
        /// <param name="id">The ID of the cryptocurrency to delete.</param>
        /// <returns>Redirects to the Tracker action after deleting the cryptocurrency.</returns>
        [HttpPost]
        [Route("/trackers/delete")]
        public async Task<IActionResult> DeleteCryptocurrency(int id)
        {
            try
            {
                await _apiHelper.DeleteAsync($"api/TrackerApi/{id}");
                return RedirectToAction("Tracker");
            }
            catch (HttpRequestException)
            {
                return View("Error");
            }
        }
    }
}