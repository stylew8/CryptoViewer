using CryptoViewer.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoViewer.BL.Repositories.Interfaces
{
    /// <summary>
    /// Interface for accessing user-related operations.
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Retrieves user details by session ID asynchronously.
        /// </summary>
        /// <param name="sessionId">The session ID used to identify the user.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the <see cref="UserDetails"/> object if found; otherwise, null.</returns>
        Task<UserDetails> GetUserDetailsBySessionId(string sessionId);

        /// <summary>
        /// Updates user details asynchronously.
        /// </summary>
        /// <param name="modelDetails">The <see cref="UserDetails"/> object containing updated user information.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task UpdateUserDetails(UserDetails modelDetails);
    }
}