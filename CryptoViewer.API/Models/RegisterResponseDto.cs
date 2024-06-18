namespace CryptoViewer.API.Models
{
    /// <summary>
    /// Represents a response DTO for user registration.
    /// </summary>
    public class RegisterResponseDto
    {
        /// <summary>
        /// Gets or sets the user ID assigned after successful registration.
        /// </summary>
        public int userId { get; set; }

        /// <summary>
        /// Gets or sets any error message in case registration fails.
        /// </summary>
        public string Error { get; set; }
    }
}