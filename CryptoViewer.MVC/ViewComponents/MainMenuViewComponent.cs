using CryptoViewer.MVC.Helpers;
using Microsoft.AspNetCore.Mvc;
using System;
using CryptoViewer.MVC.Models.Dto;

namespace CryptoViewer.MVC.ViewComponents
{
    public class MainMenuViewComponent : ViewComponent
    {
        private readonly IApiHelper api;
        private readonly IWebCookie web;

        public MainMenuViewComponent(IApiHelper api, IWebCookie apiHelper)
        {
            this.api = api;
            this.web = apiHelper;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var session = web.Get("CustomSession");
            if (session == null || string.IsNullOrEmpty(session) || string.IsNullOrWhiteSpace(session))
            {
                var guid = Guid.NewGuid();

                session = guid.ToString();

                web.AddSecure("CustomSession", session, 5);
            }


            var isLoggedIn = await api.PostAsync<IsLoggedInResponse>("api/DbSession/isLoggedInByGuid", new { sessionId = session });

            return View("Index", isLoggedIn.Result);
        }
    }
}
