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
    public class DbSession : IDbSession
    {
        private readonly IDbSessionDAL sessionDAL;

        private DbSessionModel? sessionModel = null;

        public DbSession(IDbSessionDAL sessionDal)
        {
            sessionDAL = sessionDal;
        }

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

        public async Task<Guid?> GetSessionId(int userId)
        {
            var sessionId = await sessionDAL.Get(userId);

            return sessionId.DbSessionId;
        }

        public async Task<bool> IsLoggedIn(string guid)
        {
            var x = await sessionDAL.Get(guid);

            if (x == null)
            {
                return false;
            }

            return true;
        }

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


        public async Task SetUserId(Guid sessionId, int userId)
        {
            var data = await sessionDAL.Get(sessionId);
            if (data == null)
                throw new Exception("Session not found");

            data.UserId = userId;
            await sessionDAL.Update(data.DbSessionId, userId);
        }

        public async Task Lock()
        {
            await GetSessionId();
            if (sessionModel == null)
                throw new Exception("Session is not loaded");
            await sessionDAL.Lock((Guid)sessionModel.DbSessionId);
        }
    }
}
