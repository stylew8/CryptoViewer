using CryptoViewer.MVC.Helpers;
using CryptoViewer.MVC.Middleware;
using CryptoViewer.MVC.Models.Dto;
using CryptoViewer.MVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Crypto.Engines;

namespace CryptoViewer.MVC.Controllers
{
    [SiteNotAuthorize()]
    public class LoginController : Controller
    {
        private readonly IApiHelper api;
        private readonly IWebCookie web;

        public LoginController(IApiHelper api, IWebCookie web)
        {
            this.api = api;
            this.web = web;
        }

        [Route("/register")]
        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        [Route("/submit_registration")]
        public async Task<IActionResult> Registering(RegisterViewModel model)
        {
           var id = await api.PostAsync<RegisterResponseDto>("api/Auth/register", model);

           if (!string.IsNullOrEmpty(id.Error))
           {
               ModelState.AddModelError("Error", id.Error);
               return View("Registration", id.Error);
           }

           if (id.userId > 0 && id != null)
           {
               var session = await api.PostAsync<GetSessionIdByIdDto>("api/DbSession/getSessionIdById", new { id.userId });

               if (session != null)
               {
                   web.Delete("CustomSession");

                   web.Add("CustomSession", session.result, 7);
               }

               return Redirect("/");
           }

           return Redirect("/register");
        }



        [Route("/login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [Route("/submit_login")]
        public async Task<IActionResult> Logging(LoginViewModel model)
        {
            var response = await api.PostAsync<AuthLoginDto>("api/Auth/login", new { username = model.Username, password = model.Password });

            if (response.userId > 0 && response.userId != null)
            {
                var session = await api.PostAsync<GetSessionIdByIdDto>("api/DbSession/getSessionIdById", new { userId = response.userId });

                if (session != null)
                {
                    web.Delete("CustomSession");

                    web.Add("CustomSession", session.result, 7);
                }
            }

            return Redirect("/");
        }
    }
}
