using Microsoft.AspNetCore.Mvc;

namespace CryptoViewer.MVC.Controllers
{
    public class ProfileController : Controller
    {
        [Route("/profile")]
        public IActionResult Profile()
        {
            return View();
        }
    }
}
