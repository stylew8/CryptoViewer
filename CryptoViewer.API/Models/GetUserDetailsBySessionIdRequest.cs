namespace CryptoViewer.API.Models
{
    /// <summary>
    /// Represents a request to retrieve user details by session ID.
    /// </summary>
    public class GetUserDetailsBySessionIdRequest
    {
        /// <summary>
        /// Gets or sets the session ID for which user details are requested.
        /// </summary>
        public string SessionId { get; set; }
    }
}