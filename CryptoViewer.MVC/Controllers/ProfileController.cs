using CryptoViewer.MVC.Middleware;
using Microsoft.AspNetCore.Mvc;

namespace CryptoViewer.MVC.Controllers
{
    [SiteAuthorize()]
    public class ProfileController : Controller
    {
        [Route("/profile")]
        public IActionResult Profile()
        {
            return View();
        }
    }
}
