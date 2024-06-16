using CryptoViewer.MVC.Helpers;
using CryptoViewer.MVC.Models.Dto;
using Google.Protobuf.WellKnownTypes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MySqlX.XDevAPI;

namespace CryptoViewer.MVC.Middleware
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class SiteNotAuthorizeAttribute : Attribute, IAsyncAuthorizationFilter
    {
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            IWebCookie web = context.HttpContext.RequestServices.GetService<IWebCookie>();
            IApiHelper api = context.HttpContext.RequestServices.GetService<IApiHelper>();

            IsLoggedInResponse isLoggedIn;

            var session = web.Get("CustomSession");
            if (session == null || string.IsNullOrEmpty(session) || string.IsNullOrWhiteSpace(session))
            {
                var guid = Guid.NewGuid();

                session = guid.ToString();

                web.AddSecure("CustomSession", session, 5);
            }
            else
            {
                isLoggedIn = await api.PostAsync<IsLoggedInResponse>("api/DbSession/isLoggedInByGuid", new { sessionId = session });

                if (isLoggedIn.Result )
                    context.Result = new RedirectResult("/");
            }

        }
    }
}
