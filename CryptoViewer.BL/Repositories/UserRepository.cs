using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CryptoViewer.BL.Repositories.Interfaces;
using CryptoViewer.DAL.Models;
using CryptoViewer.DAL.Repositories.Interfaces;

namespace CryptoViewer.BL.Repositories
{
    /// <summary>
    /// Repository class implementing IUserRepository for user-related operations.
    /// </summary>
    public class UserRepository : IUserRepository
    {
        private readonly IUserRepositoryDAL repo;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserRepository"/> class.
        /// </summary>
        /// <param name="userRepository">The data access layer interface for user repository operations.</param>
        public UserRepository(IUserRepositoryDAL userRepository)
        {
            repo = userRepository;
        }

        /// <summary>
        /// Retrieves user details by session ID asynchronously.
        /// </summary>
        /// <param name="sessionId">The session ID used to identify the user.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the <see cref="UserDetails"/> object if found; otherwise, null.</returns>
        public async Task<UserDetails> GetUserDetailsBySessionId(string sessionId)
        {
            return await repo.GetUserBySessionId(sessionId);
        }

        /// <summary>
        /// Updates user details asynchronously.
        /// </summary>
        /// <param name="modelDetails">The <see cref="UserDetails"/> object containing updated user information.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task UpdateUserDetails(UserDetails modelDetails)
        {
            await repo.UpdateUserDetails(modelDetails);
        }
    }
}