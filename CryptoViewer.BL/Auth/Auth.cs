using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CryptoViewer.BL.Auth.Interfaces;
using CryptoViewer.BL.Exceptions;
using CryptoViewer.BL.General;
using CryptoViewer.DAL.Auth.Interfaces;
using CryptoViewer.DAL.Models;

namespace CryptoViewer.BL.Auth
{
    /// <summary>
    /// Provides authentication and user management functionality.
    /// </summary>
    public class Auth : IAuth
    {
        private readonly IAuthDAL authDal;
        private readonly IEncrypt encrypt;
        private readonly IDbSession dbSession;

        /// <summary>
        /// Initializes a new instance of the <see cref="Auth"/> class.
        /// </summary>
        /// <param name="authDal">The data access layer for authentication operations.</param>
        /// <param name="encrypt">The service used for password encryption.</param>
        /// <param name="dbSession">The session management service.</param>
        public Auth(IAuthDAL authDal,
            IEncrypt encrypt,
            IDbSession dbSession)
        {
            this.authDal = authDal;
            this.encrypt = encrypt;
            this.dbSession = dbSession;
        }

        /// <summary>
        /// Logs in the user identified by the specified ID.
        /// </summary>
        /// <param name="id">The ID of the user to log in.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task Login(int id)
        {
            Guid sessionId = await dbSession.GetSessionId();
            await dbSession.SetUserId(sessionId, id);
        }

        /// <summary>
        /// Authenticates a user with the provided username and password.
        /// </summary>
        /// <param name="username">The username of the user.</param>
        /// <param name="password">The password of the user.</param>
        /// <returns>The ID of the authenticated user.</returns>
        /// <exception cref="AuthorizationException">Thrown when authentication fails.</exception>
        public async Task<int> Authenticate(string username, string password)
        {
            var user = await authDal.GetUserAsync(username);

            if (user != null && user.Password == encrypt.HashPassword(password, user.Salt))
            {
                await Login(user.Id);
                return user.Id;
            }
            throw new AuthorizationException();
        }

        /// <summary>
        /// Creates a new user with the provided user model and details.
        /// </summary>
        /// <param name="user">The user model containing user information.</param>
        /// <param name="details">The details of the user.</param>
        /// <returns>The ID of the created user.</returns>
        public async Task<int> CreateUser(UserModel user, UserDetails details)
        {
            user.Salt = Guid.NewGuid().ToString();
            user.Password = encrypt.HashPassword(user.Password, user.Salt);

            int? id = await authDal.CreateUserAsync(user);

            details.UserId = (int)id;
            await authDal.CreateUserDetailsAsync(details);

            if (id != null)
            {
                await Login((int)id);
            }

            return id ?? 0;
        }

        /// <summary>
        /// Validates the uniqueness of the provided email.
        /// </summary>
        /// <param name="email">The email to validate.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        /// <exception cref="DuplicateEmailException">Thrown when the email already exists.</exception>
        public async Task ValidateEmail(string email)
        {
            var user = await authDal.GetUserIdAsync(email);
            if (user != null)
                throw new DuplicateEmailException();
        }

        /// <summary>
        /// Validates the uniqueness of the provided username.
        /// </summary>
        /// <param name="username">The username to validate.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        /// <exception cref="DuplicateUsernameException">Thrown when the username already exists.</exception>
        public async Task ValidateUsername(string username)
        {
            var user = await authDal.GetUserAsync(username);
            if (user != null)
                throw new DuplicateUsernameException();
        }

        /// <summary>
        /// Registers a new user with the provided user model and details.
        /// </summary>
        /// <param name="user">The user model containing user information.</param>
        /// <param name="details">The details of the user.</param>
        /// <returns>The ID of the registered user.</returns>
        public async Task<int> Register(UserModel user, UserDetails details)
        {
            int id = 0;

            using (var scope = Helpers.CreateTransactionScope())
            {
                await dbSession.Lock();
                await ValidateEmail(details.Email);
                await ValidateUsername(user.Username);
                id = await CreateUser(user, details);
                scope.Complete();
            }

            return id;
        }
    }
}