using CryptoViewer.MVC.Helpers;
using CryptoViewer.MVC.Middleware;
using CryptoViewer.MVC.Models.Dto;
using CryptoViewer.MVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Crypto.Engines;
using System.Threading.Tasks;

namespace CryptoViewer.MVC.Controllers
{
    /// <summary>
    /// Controller responsible for handling user authentication and registration actions.
    /// </summary>
    [SiteNotAuthorize()]
    public class LoginController : Controller
    {
        private readonly IApiHelper api;
        private readonly IWebCookie web;

        /// <summary>
        /// Constructor to initialize the LoginController with required dependencies.
        /// </summary>
        /// <param name="api">The API helper instance for making API calls.</param>
        /// <param name="web">The web cookie helper instance for managing session cookies.</param>
        public LoginController(IApiHelper api, IWebCookie web)
        {
            this.api = api;
            this.web = web;
        }

        /// <summary>
        /// Action method for rendering the registration view.
        /// </summary>
        /// <returns>The registration view.</returns>
        [Route("/register")]
        public IActionResult Registration()
        {
            return View();
        }

        /// <summary>
        /// Action method for handling registration form submission.
        /// </summary>
        /// <param name="model">The registration view model containing user data.</param>
        /// <returns>Redirects to home page on successful registration; otherwise, returns to registration page with error.</returns>
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

        /// <summary>
        /// Action method for rendering the login view.
        /// </summary>
        /// <returns>The login view.</returns>
        [Route("/login")]
        public IActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// Action method for handling login form submission.
        /// </summary>
        /// <param name="model">The login view model containing user credentials.</param>
        /// <returns>Redirects to home page on successful login; otherwise, returns to login page.</returns>
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