namespace CryptoViewer.API.Models
{
    /// <summary>
    /// Represents a request model to update user details by ID.
    /// </summary>
    public class UpdateUserDetailsByIdRequest
    {
        /// <summary>
        /// Gets or sets the ID of the user whose details are to be updated.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the first name of the user.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name of the user.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the email address of the user.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the address of the user.
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Gets or sets the modified date and time of the user details (default is UTC).
        /// </summary>
        public DateTime ModifiedAt { get; set; } = DateTime.UtcNow;
    }
}