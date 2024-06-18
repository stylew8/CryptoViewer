using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CryptoViewer.DAL.Helpers;
using CryptoViewer.DAL.Models;
using CryptoViewer.DAL.Repositories.Interfaces;

namespace CryptoViewer.DAL.Repositories
{
    /// <summary>
    /// Repository implementation for interacting with user-related data in the database.
    /// </summary>
    public class UserRepositoryDAL : IUserRepositoryDAL
    {
        private readonly IDbHelper db;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserRepositoryDAL"/> class.
        /// </summary>
        /// <param name="db">The database helper instance for executing queries.</param>
        public UserRepositoryDAL(IDbHelper db)
        {
            this.db = db ?? throw new ArgumentNullException(nameof(db));
        }

        /// <summary>
        /// Retrieves user details based on the session ID from the database.
        /// </summary>
        /// <param name="sessionId">The session ID used to identify the user.</param>
        /// <returns>The <see cref="UserDetails"/> object containing user details.</returns>
        public async Task<UserDetails> GetUserBySessionId(string sessionId)
        {
            string sql = "SELECT * FROM UserDetails " +
                         "JOIN DbSession AS s ON UserDetails.UserId = s.UserId " +
                         "WHERE s.DbSessionId = @sessionId;";

            return await db.QueryScalarAsync<UserDetails>(sql, new { sessionId = sessionId });
        }

        /// <summary>
        /// Updates user details in the database.
        /// </summary>
        /// <param name="model">The <see cref="UserDetails"/> object containing updated user details.</param>
        public async Task UpdateUserDetails(UserDetails model)
        {
            string sql = "UPDATE UserDetails " +
                         "SET FirstName = @FirstName, " +
                         "LastName = @LastName, " +
                         "Email = @Email, " +
                         "Address = @Address, " +
                         "ModifiedAt = @ModifiedAt " +
                         "WHERE Id = @Id";

            await db.ExecuteAsync(sql, model);
        }
    }
}