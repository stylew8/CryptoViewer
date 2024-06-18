using CryptoViewer.MVC.Helpers;
using CryptoViewer.MVC.Models.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Threading.Tasks;

namespace CryptoViewer.MVC.Middleware
{
    /// <summary>
    /// Attribute to authorize access to controllers or actions based on session existence.
    /// Redirects to login page if session is not valid or missing.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class SiteAuthorize : Attribute, IAsyncAuthorizationFilter
    {
        /// <summary>
        /// Asynchronous method called during authorization process.
        /// </summary>
        /// <param name="context">Authorization filter context containing HTTP context and authorization state.</param>
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            // Retrieve necessary services through dependency injection
            IWebCookie web = context.HttpContext.RequestServices.GetService<IWebCookie>();
            IApiHelper api = context.HttpContext.RequestServices.GetService<IApiHelper>();

            // Check if CustomSession cookie exists and retrieve its value
            var session = web.Get("CustomSession");

            // If session doesn't exist or is invalid, create a new session
            if (string.IsNullOrEmpty(session))
            {
                var guid = Guid.NewGuid();
                session = guid.ToString();

                // Add the new session ID as a secure cookie with 5 days expiration
                web.AddSecure("CustomSession", session, 5);
            }

            // Call API to check if the session ID is logged in
            var isLoggedIn = await api.PostAsync<IsLoggedInResponse>("api/DbSession/isLoggedInByGuid", new { sessionId = session });

            // If the session is not logged in, redirect to the login page
            if (!isLoggedIn.Result)
            {
                context.Result = new RedirectResult("/login");
            }
        }
    }
}
