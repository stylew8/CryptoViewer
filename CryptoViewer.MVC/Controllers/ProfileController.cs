using CryptoViewer.MVC.Helpers;
using CryptoViewer.MVC.Middleware;
using CryptoViewer.MVC.Models.Dto;
using CryptoViewer.MVC.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CryptoViewer.MVC.Controllers
{
    [SiteAuthorize()]
    public class ProfileController : Controller
    {
        private readonly IApiHelper api;
        private readonly IWebCookie web;

        public ProfileController(IApiHelper api, IWebCookie web)
        {
            this.api = api;
            this.web = web;
        }

        [Route("/profile")]
        public async Task<IActionResult> Profile()
        {
            var sessiondId = web.Get("CustomSession");
            if (sessiondId == null)
            {
                return Redirect("/");
            }

            var modelDto = await api.PostAsync<GetUserDetailsResponseDto>("api/User/UserDetailsBySessionId",
                new GetUserDetailsRequest() { SessionId = sessiondId });

            return View(new ProfileViewModel()
            {
                Id = modelDto.result.Id,
                Address = modelDto.result.Address,
                Email = modelDto.result.Email,
                FirstName = modelDto.result.FirstName,
                LastName = modelDto.result.LastName,
            });
        }


        [HttpPost]
        [Route("/submit_update_profile")]
        public async Task<IActionResult> UpdateProfile(ProfileViewModel model)
        {
            await api.PostAsync<BaseDto>("api/User/UpdateUserDetailsById", model);

            return Redirect("/profile");
        }
    }
}
