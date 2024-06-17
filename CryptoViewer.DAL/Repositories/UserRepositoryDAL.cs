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
    public class UserRepositoryDAL : IUserRepositoryDAL
    {
        private readonly IDbHelper db;

        public UserRepositoryDAL(IDbHelper db)
        {
            this.db = db;
        }

        public async Task<UserDetails> GetUserBySessionId(string sessionId)
        {
            string sql = "SELECT * FROM UserDetails " +
                         "JOIN DbSession AS s ON UserDetails.UserId = s.UserId " +
                         "WHERE s.DbSessionId = @sessionId;";

            return await db.QueryScalarAsync<UserDetails>(sql, new { sessionId = sessionId });
        }

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
