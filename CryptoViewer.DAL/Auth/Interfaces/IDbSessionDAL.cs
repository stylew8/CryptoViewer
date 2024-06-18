using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CryptoViewer.DAL.Models;

namespace CryptoViewer.DAL.Auth.Interfaces
{
    /// <summary>
    /// Interface for Data Access Layer operations related to session management.
    /// </summary>
    public interface IDbSessionDAL
    {
        /// <summary>
        /// Retrieves a session by session ID asynchronously.
        /// </summary>
        /// <param name="sessionId">The session ID to retrieve.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the <see cref="DbSessionModel"/> object if found; otherwise, null.</returns>
        Task<DbSessionModel?> Get(string sessionId);

        /// <summary>
        /// Retrieves a session by session ID asynchronously.
        /// </summary>
        /// <param name="sessionId">The session ID to retrieve.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the <see cref="DbSessionModel"/> object if found; otherwise, null.</returns>
        Task<DbSessionModel?> Get(Guid sessionId);

        /// <summary>
        /// Retrieves a session by user ID asynchronously.
        /// </summary>
        /// <param name="userId">The ID of the user associated with the session.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the <see cref="DbSessionModel"/> object if found; otherwise, null.</returns>
        Task<DbSessionModel?> Get(int userId);

        /// <summary>
        /// Updates the user ID associated with a session asynchronously.
        /// </summary>
        /// <param name="dbSessionID">The ID of the session to update.</param>
        /// <param name="userId">The ID of the user to associate with the session.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task Update(Guid dbSessionID, int userId);

        /// <summary>
        /// Extends the expiration of a session asynchronously.
        /// </summary>
        /// <param name="dbSessionID">The ID of the session to extend.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task Extend(Guid dbSessionID);

        /// <summary>
        /// Creates a new session asynchronously.
        /// </summary>
        /// <param name="model">The <see cref="DbSessionModel"/> object representing the session to create.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task Create(DbSessionModel model);

        /// <summary>
        /// Locks a session to prevent further modifications asynchronously.
        /// </summary>
        /// <param name="sessionId">The ID of the session to lock.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task Lock(Guid sessionId);
    }
}