using Microsoft.AspNetCore.Mvc;

namespace CryptoViewer.MVC.Controllers
{
    public class ChartController : Controller
    {
        [Route("/btc")]
        public IActionResult BitcChart()
        {
            return View();
        }

        [Route("/eth")]
        public IActionResult EthChart()
        {
            return View();
        }
    }
}
