using Microsoft.AspNetCore.Http;
using System;
using System.Linq;

namespace CryptoViewer.MVC.Helpers
{
    /// <summary>
    /// Helper class for managing HTTP cookies.
    /// </summary>
    public class WebCookie : IWebCookie
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public WebCookie(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }

        /// <summary>
        /// Adds a secure HTTP cookie with the specified name and value.
        /// </summary>
        /// <param name="cookieName">The name of the cookie.</param>
        /// <param name="value">The value to store in the cookie.</param>
        /// <param name="days">Optional number of days until the cookie expires (default is 0, indicating session cookie).</param>
        public void AddSecure(string cookieName, string value, int days = 0)
        {
            CookieOptions options = new CookieOptions
            {
                Path = "/",
                HttpOnly = true,
                Secure = true
            };

            if (days > 0)
            {
                options.Expires = DateTimeOffset.UtcNow.AddDays(days);
            }

            _httpContextAccessor.HttpContext?.Response.Cookies.Append(cookieName, value, options);
        }

        /// <summary>
        /// Adds an HTTP cookie with the specified name and value.
        /// </summary>
        /// <param name="cookieName">The name of the cookie.</param>
        /// <param name="value">The value to store in the cookie.</param>
        /// <param name="days">Optional number of days until the cookie expires (default is 0, indicating session cookie).</param>
        public void Add(string cookieName, string value, int days = 0)
        {
            CookieOptions options = new CookieOptions
            {
                Path = "/"
            };

            if (days > 0)
            {
                options.Expires = DateTimeOffset.UtcNow.AddDays(days);
            }

            _httpContextAccessor.HttpContext?.Response.Cookies.Append(cookieName, value, options);
        }

        /// <summary>
        /// Deletes the HTTP cookie with the specified name.
        /// </summary>
        /// <param name="cookieName">The name of the cookie to delete.</param>
        public void Delete(string cookieName)
        {
            _httpContextAccessor.HttpContext?.Response.Cookies.Delete(cookieName);
        }

        /// <summary>
        /// Retrieves the value of the HTTP cookie with the specified name.
        /// </summary>
        /// <param name="cookieName">The name of the cookie to retrieve.</param>
        /// <returns>The value of the cookie if found; otherwise, null.</returns>
        public string Get(string cookieName)
        {
            var cookieValue = _httpContextAccessor.HttpContext?.Request?.Cookies[cookieName];
            return cookieValue;
        }
    }
}
