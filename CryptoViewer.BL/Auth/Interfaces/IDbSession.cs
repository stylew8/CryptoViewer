using CryptoViewer.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoViewer.BL.Auth.Interfaces
{
    /// <summary>
    /// Interface defining operations related to user session management.
    /// </summary>
    public interface IDbSession
    {
        /// <summary>
        /// Retrieves a new session ID.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation, returning the generated session ID.</returns>
        Task<Guid> GetSessionId();

        /// <summary>
        /// Sets the user ID associated with the provided session ID.
        /// </summary>
        /// <param name="sessionId">The session ID to set the user ID for.</param>
        /// <param name="userId">The ID of the user to associate with the session.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task SetUserId(Guid sessionId, int userId);

        /// <summary>
        /// Locks the session to prevent concurrent access.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task Lock();

        /// <summary>
        /// Retrieves the session ID associated with the provided user ID.
        /// </summary>
        /// <param name="userId">The ID of the user to retrieve the session ID for.</param>
        /// <returns>A task that represents the asynchronous operation, returning the session ID if found; otherwise, null.</returns>
        Task<Guid?> GetSessionId(int userId);

        /// <summary>
        /// Checks if a session identified by the provided GUID is logged in.
        /// </summary>
        /// <param name="guid">The GUID identifying the session.</param>
        /// <returns>A task that represents the asynchronous operation, returning true if the session is logged in; otherwise, false.</returns>
        Task<bool> IsLoggedIn(string guid);
    }
}