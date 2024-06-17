using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CryptoViewer.DAL.Models;

namespace CryptoViewer.DAL.Repositories.Interfaces
{
    public interface IUserRepositoryDAL
    {
        Task<UserDetails> GetUserBySessionId(string sessionId);

        Task UpdateUserDetails(UserDetails model);
    }
}
