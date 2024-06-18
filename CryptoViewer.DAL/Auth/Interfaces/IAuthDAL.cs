using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CryptoViewer.DAL.Models;

namespace CryptoViewer.DAL.Auth.Interfaces
{
    /// <summary>
    /// Interface for Data Access Layer operations related to authentication and user management.
    /// </summary>
    public interface IAuthDAL
    {
        /// <summary>
        /// Creates a new user asynchronously.
        /// </summary>
        /// <param name="model">The <see cref="UserModel"/> object containing user information.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the ID of the newly created user if successful; otherwise, null.</returns>
        Task<int?> CreateUserAsync(UserModel model);

        /// <summary>
        /// Creates user details asynchronously.
        /// </summary>
        /// <param name="model">The <see cref="UserDetails"/> object containing user details information.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the ID of the newly created user details if successful; otherwise, null.</returns>
        Task<int?> CreateUserDetailsAsync(UserDetails model);

        /// <summary>
        /// Retrieves a user by ID asynchronously.
        /// </summary>
        /// <param name="id">The ID of the user to retrieve.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the <see cref="UserModel"/> object if found; otherwise, null.</returns>
        Task<UserModel?> GetUserAsync(int id);

        /// <summary>
        /// Retrieves the ID of a user by email asynchronously.
        /// </summary>
        /// <param name="email">The email address of the user.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the ID of the user if found; otherwise, null.</returns>
        Task<int?> GetUserIdAsync(string email);

        /// <summary>
        /// Retrieves a user by username asynchronously.
        /// </summary>
        /// <param name="username">The username of the user.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the <see cref="UserModel"/> object if found; otherwise, null.</returns>
        Task<UserModel?> GetUserAsync(string username);
    }
}