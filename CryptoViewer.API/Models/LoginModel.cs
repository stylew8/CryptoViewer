namespace CryptoViewer.API.Models
{
    /// <summary>
    /// Represents a login request model.
    /// </summary>
    public class LoginModel
    {
        /// <summary>
        /// Gets or sets the username for authentication.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the password for authentication.
        /// </summary>
        public string Password { get; set; }
    }
}