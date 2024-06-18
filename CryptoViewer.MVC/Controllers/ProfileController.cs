using CryptoViewer.MVC.Helpers;
using CryptoViewer.MVC.Middleware;
using CryptoViewer.MVC.Models.Dto;
using CryptoViewer.MVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CryptoViewer.MVC.Controllers
{
    /// <summary>
    /// Controller responsible for handling user profile actions.
    /// </summary>
    [SiteAuthorize()]
    public class ProfileController : Controller
    {
        private readonly IApiHelper api;
        private readonly IWebCookie web;

        /// <summary>
        /// Constructor to initialize the ProfileController with required dependencies.
        /// </summary>
        /// <param name="api">The API helper instance for making API calls.</param>
        /// <param name="web">The web cookie helper instance for managing session cookies.</param>
        public ProfileController(IApiHelper api, IWebCookie web)
        {
            this.api = api;
            this.web = web;
        }

        /// <summary>
        /// Action method for rendering the user profile view.
        /// </summary>
        /// <returns>The profile view if session exists; otherwise, redirects to the home page.</returns>
        [Route("/profile")]
        public async Task<IActionResult> Profile()
        {
            var sessionId = web.Get("CustomSession");
            if (sessionId == null)
            {
                return Redirect("/");
            }

            var modelDto = await api.PostAsync<GetUserDetailsResponseDto>("api/User/UserDetailsBySessionId",
                new GetUserDetailsRequest() { SessionId = sessionId });

            return View(new ProfileViewModel()
            {
                Id = modelDto.result.Id,
                Address = modelDto.result.Address,
                Email = modelDto.result.Email,
                FirstName = modelDto.result.FirstName,
                LastName = modelDto.result.LastName,
            });
        }

        /// <summary>
        /// Action method for handling profile update form submission.
        /// </summary>
        /// <param name="model">The profile view model containing updated user details.</param>
        /// <returns>Redirects to the profile page after updating user details.</returns>
        [HttpPost]
        [Route("/submit_update_profile")]
        public async Task<IActionResult> UpdateProfile(ProfileViewModel model)
        {
            await api.PostAsync<BaseDto>("api/User/UpdateUserDetailsById", model);

            return Redirect("/profile");
        }
    }
}