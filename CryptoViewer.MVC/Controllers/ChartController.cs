using Microsoft.AspNetCore.Mvc;

namespace CryptoViewer.MVC.Controllers
{
    /// <summary>
    /// Controller responsible for handling chart-related views and actions.
    /// </summary>
    public class ChartController : Controller
    {
        /// <summary>
        /// Displays the chart view for a specific cryptocurrency symbol.
        /// </summary>
        /// <param name="symbol">The symbol of the cryptocurrency.</param>
        /// <returns>The view displaying the chart for the specified cryptocurrency.</returns>
        [Route("/chart/{symbol}")]
        public IActionResult Chart(string symbol)
        {
            // Store the symbol in ViewData to be accessed by the view
            ViewData["Symbol"] = symbol;

            // Return the view named "Chart"
            return View();
        }
    }
}