namespace CryptoViewer.API.Models
{
    /// <summary>
    /// Represents a request to retrieve session ID by user ID.
    /// </summary>
    public class GetSessionByIdRequest
    {
        /// <summary>
        /// Gets or sets the user ID for which the session ID is requested.
        /// </summary>
        public int userId { get; set; }
    }
}