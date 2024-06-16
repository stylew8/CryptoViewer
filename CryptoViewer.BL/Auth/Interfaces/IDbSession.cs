using CryptoViewer.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoViewer.BL.Auth.Interfaces
{
    public interface IDbSession
    {
        Task<Guid> GetSessionId();

        Task SetUserId(Guid sessionId, int userId);

        Task Lock();
        Task<Guid?> GetSessionId(int userId);
        Task<bool> IsLoggedIn(string guid);

    }
}
