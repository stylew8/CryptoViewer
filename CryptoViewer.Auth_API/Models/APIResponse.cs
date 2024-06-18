using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CryptoViewer.Auth_API.Models
{
    /// <summary>
    /// Represents a generic API response structure.
    /// </summary>
    public class APIResponse
    {
        /// <summary>
        /// Gets or sets the HTTP status code of the API response.
        /// </summary>
        public HttpStatusCode StatusCode;

        /// <summary>
        /// Gets or sets a flag indicating whether the API operation was successful.
        /// </summary>
        public bool IsSuccess;

        /// <summary>
        /// Gets or sets a list of error messages in case of failure.
        /// </summary>
        public List<string> ErrorMessages;

        /// <summary>
        /// Gets or sets the result object of the API operation.
        /// </summary>
        public object Result;

        /// <summary>
        /// Creates a new instance of APIResponse for a BadRequest scenario with specified error messages.
        /// </summary>
        /// <param name="errorMessages">The list of error messages explaining the BadRequest.</param>
        /// <returns>A new APIResponse instance indicating BadRequest.</returns>
        public static APIResponse CreateBadRequest(List<string> errorMessages)
        {
            return new APIResponse
            {
                IsSuccess = false,
                StatusCode = HttpStatusCode.BadRequest,
                ErrorMessages = errorMessages,
                Result = null
            };
        }
    }
}