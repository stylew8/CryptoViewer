using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CryptoViewer.DAL.Models;

namespace CryptoViewer.DAL.Auth.Interfaces
{
    public interface IDbSessionDAL
    {
        Task<DbSessionModel?> Get(Guid sessionId);

        Task Update(Guid dbSessionID , int userid);

        Task Extend(Guid dbSessionID);

        Task Create(DbSessionModel model);

        Task Lock(Guid sessionId);
    }
}
