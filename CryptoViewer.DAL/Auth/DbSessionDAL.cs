using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CryptoViewer.DAL.Auth.Interfaces;
using CryptoViewer.DAL.Helpers;
using CryptoViewer.DAL.Models;

namespace CryptoViewer.DAL.Auth
{
    /// <summary>
    /// Implementation of IDbSessionDAL for managing database sessions.
    /// </summary>
    public class DbSessionDAL : IDbSessionDAL
    {
        private readonly IDbHelper dbHelper;

        /// <summary>
        /// Initializes a new instance of the <see cref="DbSessionDAL"/> class.
        /// </summary>
        /// <param name="dbHelper">The database helper instance.</param>
        public DbSessionDAL(IDbHelper dbHelper)
        {
            this.dbHelper = dbHelper;
        }

        /// <summary>
        /// Creates a new database session.
        /// </summary>
        /// <param name="model">The session model to create.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task Create(DbSessionModel model)
        {
            string sql = @"insert into DbSession (DbSessionID, SessionData, Created, LastAccessed, UserId)
                    values (@DbSessionID, @SessionData, @Created, @LastAccessed, @UserId)";

            await dbHelper.ExecuteAsync(sql, model);
        }

        /// <summary>
        /// Retrieves a database session by session ID.
        /// </summary>
        /// <param name="sessionId">The session ID.</param>
        /// <returns>The database session model if found; otherwise, null.</returns>
        public async Task<DbSessionModel?> Get(string sessionId)
        {
            var x = Guid.Parse(sessionId);

            string sql = @"select DbSessionId, SessionData, Created, LastAccessed, UserId from DbSession where DbSessionID = @sessionId";
            var sessions = await dbHelper.QueryAsync<DbSessionModel?>(sql, new { sessionId = x });
            return sessions.FirstOrDefault();
        }

        /// <summary>
        /// Retrieves a database session by session ID.
        /// </summary>
        /// <param name="sessionId">The session ID.</param>
        /// <returns>The database session model if found; otherwise, null.</returns>
        public async Task<DbSessionModel?> Get(Guid sessionId)
        {
            string sql = @"select DbSessionId, SessionData, Created, LastAccessed, UserId from DbSession where DbSessionID = @sessionId";
            var sessions = await dbHelper.QueryAsync<DbSessionModel?>(sql, new { sessionId });
            return sessions.FirstOrDefault();
        }

        /// <summary>
        /// Retrieves a database session by user ID.
        /// </summary>
        /// <param name="userId">The user ID.</param>
        /// <returns>The database session model if found; otherwise, null.</returns>
        public async Task<DbSessionModel?> Get(int userId)
        {
            string sql = @"select DbSessionId, SessionData, Created, LastAccessed, UserId from DbSession where UserId = @userId";
            var sessions = await dbHelper.QueryAsync<DbSessionModel?>(sql, new { userId });
            return sessions.FirstOrDefault();
        }

        /// <summary>
        /// Locks a database session for exclusive access.
        /// </summary>
        /// <param name="sessionId">The session ID to lock.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task Lock(Guid sessionId)
        {
            string sql = @"select DbSessionId from DbSession where DbSessionID = @sessionId for update";
            await dbHelper.QueryAsync<DbSessionModel>(sql, new { sessionId });
        }

        /// <summary>
        /// Updates the user ID associated with a database session.
        /// </summary>
        /// <param name="dbSessionID">The session ID.</param>
        /// <param name="userId">The new user ID.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task Update(Guid dbSessionID, int userId)
        {
            string sql = @"update DbSession
                   set UserId = @userId
                   where DbSessionId = @dbSessionID";

            await dbHelper.ExecuteAsync(sql, new { dbSessionID, userId });
        }

        /// <summary>
        /// Extends the last accessed time of a database session.
        /// </summary>
        /// <param name="dbSessionID">The session ID.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task Extend(Guid dbSessionID)
        {
            string sql = @"update DbSession
                    set LastAccessed = @lastAccessed
                    where DbSessionId = @dbSessionID";

            await dbHelper.ExecuteAsync(sql, new { dbSessionID, lastAccessed = DateTime.UtcNow });
        }
    }
}