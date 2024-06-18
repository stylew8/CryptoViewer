using CryptoViewer.MVC.Helpers;
using Microsoft.AspNetCore.Mvc;
using System;
using CryptoViewer.MVC.Models.Dto;
using System.Threading.Tasks;

namespace CryptoViewer.MVC.ViewComponents
{
    /// <summary>
    /// View component responsible for rendering the main menu based on user session.
    /// </summary>
    public class MainMenuViewComponent : ViewComponent
    {
        private readonly IApiHelper api;
        private readonly IWebCookie web;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainMenuViewComponent"/> class.
        /// </summary>
        /// <param name="api">Instance of <see cref="IApiHelper"/> for API interactions.</param>
        /// <param name="web">Instance of <see cref="IWebCookie"/> for handling web cookies.</param>
        public MainMenuViewComponent(IApiHelper api, IWebCookie web)
        {
            this.api = api;
            this.web = web;
        }

        /// <summary>
        /// Asynchronous method invoked to render the view component.
        /// </summary>
        /// <returns>An asynchronous task that returns an <see cref="IViewComponentResult"/>.</returns>
        public async Task<IViewComponentResult> InvokeAsync()
        {
            // Get the session ID from the web cookie
            var session = web.Get("CustomSession");

            // If session does not exist, generate a new GUID and set it as the session ID cookie
            if (string.IsNullOrEmpty(session))
            {
                var guid = Guid.NewGuid();
                session = guid.ToString();
                web.AddSecure("CustomSession", session, 5); // Adding a secure cookie with 5 days expiration

                return View("Index", false); // Return the view with isLoggedIn = false
            }

            // Check if the session ID is logged in by calling an API
            var isLoggedIn = await api.PostAsync<IsLoggedInResponse>("api/DbSession/isLoggedInByGuid", new { sessionId = session });

            return View("Index", isLoggedIn.Result); // Return the view with the isLoggedIn status
        }
    }
}
