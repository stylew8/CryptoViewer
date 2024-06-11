using Microsoft.AspNetCore.Mvc;

namespace CryptoViewer.MVC.Controllers
{
    public class LoginController : Controller
    {
        [Route("/register")]
        public IActionResult Registration()
        {
            return View();
        }
        [Route("/login")]
        public IActionResult Login()
        {
            return View();
        }
    }
}
