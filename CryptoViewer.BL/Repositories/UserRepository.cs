using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CryptoViewer.BL.Repositories.Interfaces;
using CryptoViewer.DAL.Models;
using CryptoViewer.DAL.Repositories.Interfaces;
using Microsoft.VisualBasic;

namespace CryptoViewer.BL.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IUserRepositoryDAL repo;

        public UserRepository(IUserRepositoryDAL userRepository)
        {
            repo = userRepository;
        }

        public async Task<UserDetails> GetUserDetailsBySessionId(string sessionId)
        {
            return await repo.GetUserBySessionId(sessionId);
        }

        public async Task UpdateUserDetails(UserDetails modelDetails)
        {
            await repo.UpdateUserDetails(modelDetails);
        }
    }
}
