using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using CryptoViewer.BL.Auth.Interfaces;
using CryptoViewer.BL.General;
using CryptoViewer.DAL.Auth;
using CryptoViewer.DAL.Auth.Interfaces;
using CryptoViewer.DAL.Models;

namespace CryptoViewer.BL.Auth
{
    /// <summary>
    /// Manages database sessions for user authentication and authorization.
    /// </summary>
    public class DbSession : IDbSession
    {
        private readonly IDbSessionDAL sessionDAL;
        private DbSessionModel? sessionModel = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="DbSession"/> class.
        /// </summary>
        /// <param name="sessionDal">The data access layer for session operations.</param>
        public DbSession(IDbSessionDAL sessionDal)
        {
            sessionDAL = sessionDal;
        }

        /// <summary>
        /// Retrieves a session ID. Creates a new session if one does not exist.
        /// </summary>
        /// <returns>The session ID.</returns>
        public async Task<Guid> GetSessionId()
        {
            if (sessionModel != null)
                return sessionModel.DbSessionId;

            Guid sessionId = Guid.NewGuid();

            var data = await sessionDAL.Get(sessionId);
            if (data == null)
            {
                data = await CreateSession();
            }

            await sessionDAL.Extend(data.DbSessionId);
            sessionModel = data;
            return data.DbSessionId;
        }

        /// <summary>
        /// Retrieves the session ID associated with the specified user ID.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <returns>The session ID associated with the user, if found; otherwise, <c>null</c>.</returns>
        public async Task<Guid?> GetSessionId(int userId)
        {
            var sessionId = await sessionDAL.Get(userId);

            return sessionId.DbSessionId;
        }

        /// <summary>
        /// Checks if a session identified by its GUID is logged in.
        /// </summary>
        /// <param name="guid">The GUID of the session to check.</param>
        /// <returns><c>true</c> if the session is logged in; otherwise, <c>false</c>.</returns>
        public async Task<bool> IsLoggedIn(string guid)
        {
            var x = await sessionDAL.Get(guid);

            return x != null;
        }

        /// <summary>
        /// Creates a new session.
        /// </summary>
        /// <returns>The created session.</returns>
        private async Task<DbSessionModel> CreateSession()
        {
            var data = new DbSessionModel()
            {
                DbSessionId = Guid.NewGuid(),
                Created = DateTime.UtcNow,
                LastAccessed = DateTime.UtcNow
            };
            await sessionDAL.Create(data);
            return data;
        }

        /// <summary>
        /// Sets the user ID associated with a session.
        /// </summary>
        /// <param name="sessionId">The session ID.</param>
        /// <param name="userId">The user ID to associate with the session.</param>
        public async Task SetUserId(Guid sessionId, int userId)
        {
            var data = await sessionDAL.Get(sessionId);
            if (data == null)
                throw new Exception("Session not found");

            data.UserId = userId;
            await sessionDAL.Update(data.DbSessionId, userId);
        }

        /// <summary>
        /// Locks the current session.
        /// </summary>
        public async Task Lock()
        {
            await GetSessionId();
            if (sessionModel == null)
                throw new Exception("Session is not loaded");
            await sessionDAL.Lock((Guid)sessionModel.DbSessionId);
        }
    }
}