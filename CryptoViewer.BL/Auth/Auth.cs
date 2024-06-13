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
    public class Auth : IAuth
    {
        private readonly IAuthDAL authDal;
        private readonly IEncrypt encrypt;
        private readonly IDbSession dbSession;


        public Auth(IAuthDAL authDal,
            IEncrypt encrypt,
            IDbSession dbSession)
        {
            this.authDal = authDal;
            this.encrypt = encrypt;
            this.dbSession = dbSession;
        }

        public async Task Login(int id)
        {
            Guid sessionId = await dbSession.GetSessionId();
            await dbSession.SetUserId(sessionId, id);
        }


        public async Task<int> Authenticate(string username, string password)
        {
            var user = await authDal.GetUserAsync(username);

            if (user != null && user.Password == encrypt.HashPassword(password, user.Salt))
            {
                await Login(user.Id);  // Убедитесь, что здесь правильно используется sessionId
                return user.Id;
            }
            throw new AuthorizationException();
        }

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


        public async Task ValidateEmail(string email)
        {
            var user = await authDal.GetUserIdAsync(email);
            if (user != null)
                throw new DuplicateEmailException();
        }

        public async Task ValidateUsername(string username)
        {
            var user = await authDal.GetUserAsync(username);
            if (user != null)
                throw new DuplicateUsernameException();
        }


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
