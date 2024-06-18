namespace CryptoViewer.API.Models
{
    /// <summary>
    /// Represents a request to check if a session ID is logged in.
    /// </summary>
    public class IsLoggedInRequest
    {
        /// <summary>
        /// Gets or sets the session ID to check for login status.
        /// </summary>
        public string sessionId { get; set; }
    }
}