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
    public class DbSessionDAL : IDbSessionDAL
    {
        private readonly IDbHelper dbHelper;

        public DbSessionDAL(IDbHelper dbHelper)
        {
            this.dbHelper = dbHelper;
        }

        public async Task Create(DbSessionModel model)
        {
            string sql = @"insert into DbSession (DbSessionID, SessionData, Created, LastAccessed, UserId)
                    values (@DbSessionID, @SessionData, @Created, @LastAccessed, @UserId)"
            ;

            await dbHelper.ExecuteAsync(sql, model);
        }

        public async Task<DbSessionModel?> Get(Guid sessionId)
        {
            string sql = @"select DbSessionId, SessionData, Created, LastAccessed, UserId from DbSession where DbSessionID = @sessionId";
            var sessions = await dbHelper.QueryAsync<DbSessionModel?>(sql, new { sessionId });
            return sessions.FirstOrDefault();
        }

        public async Task Lock(Guid sessionId)
        {
            string sql = @"select DbSessionId from DbSession where DbSessionID = @sessionId for update";
            await dbHelper.QueryAsync<DbSessionModel>(sql, new { sessionId });
        }

        public async Task Update(Guid dbSessionID, int userId)
        {
            string sql = @"update DbSession
                   set UserId = @userId
                   where DbSessionId = @dbSessionID";

            await dbHelper.ExecuteAsync(sql, new { dbSessionID, userId });
        }


        public async Task Extend(Guid dbSessionID)
        {
            string sql = @"update DbSession
                    set LastAccessed = @lastAccessed
                    where DbSessionId = @dbSessionID"
            ;

            await dbHelper.ExecuteAsync(sql, new { dbSessionID, lastAccessed = DateTime.UtcNow });
        }
    }
}
