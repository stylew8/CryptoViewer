using Microsoft.AspNetCore.Mvc;

namespace CryptoViewer.MVC.Controllers
{
    public class ChartController : Controller
    {
        [Route("/chart/{symbol}")]
        public IActionResult Chart(string symbol)
        {
            ViewData["Symbol"] = symbol;
            return View();
        }
    }
}
