using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoViewer.BL.Auth
{
    /// <summary>
    /// Contains constants related to authentication.
    /// </summary>
    public class AuthConstants
    {
        /// <summary>
        /// The parameter name used to identify the authenticated user session.
        /// </summary>
        public const string AuthSessionParamName = "userId";

        /// <summary>
        /// The name of the custom session cookie used for authentication.
        /// </summary>
        public const string SessionCookieName = "CustomSessionId";
    }
}