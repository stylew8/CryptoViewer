﻿using Microsoft.AspNetCore.Mvc;

namespace CryptoViewer.MVC.Controllers
{
    public class TrackerController : Controller
    {
        [Route("/trackers")]
        public IActionResult Tracker()
        {
            return View();
        }
    }
}
