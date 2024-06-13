using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CryptoViewer.DAL.Auth.Interfaces;
using CryptoViewer.DAL.Exceptions;
using CryptoViewer.DAL.Helpers;
using CryptoViewer.DAL.Models;

namespace CryptoViewer.DAL.Auth
{
    public class AuthDAL : IAuthDAL
    {
        private readonly IDbHelper dbHelper;
        private readonly ILoggingService loggingService;

        public AuthDAL(IDbHelper dbHelper, ILoggingService loggingService)
        {
            this.dbHelper = dbHelper;
            this.loggingService = loggingService;
        }


        public async Task<int?> CreateUserAsync(UserModel model)
        {
            try
            {
                string sql = "insert into User(Username, Password, Salt, CreatedAt, ModifiedAt) values(@Username, @Password, @Salt, @CreatedAt, @ModifiedAt); " +
                             "SELECT LAST_INSERT_ID();";

                return await dbHelper.QueryScalarAsync<int?>(sql, model);
            }
            catch (Exception e)
            {
                loggingService.Log(e.Message);

                throw new CreatingUserException();
            }
        }

        public async Task<int?> CreateUserDetailsAsync(UserDetails model)
        {
            try
            {
                string sql = "insert into UserDetails(UserId, FirstName, LastName, Email, Address, CreatedAt, ModifiedAt) " +
                             "values(@UserId, @FirstName, @LastName, @Email, @Address, @CreatedAt, @ModifiedAt); " +
                             "SELECT LAST_INSERT_ID();";

                return await dbHelper.QueryScalarAsync<int?>(sql, model);
            }
            catch (Exception e)
            {
                loggingService.Log(e.Message);

                throw new CreatingUserDetailsException();
            }
        }

        public async Task<UserModel?> GetUserAsync(int id)
        {
            //try
            //{
                string sql = "select * from User where Id = @Id";

                return await dbHelper.QueryScalarAsync<UserModel>(sql, new {Id = id});
            //}
            //catch (Exception e)
            //{
            //    loggingService.Log(e.Message);

            //    throw new GettingUserException();
            //}
        }

        public async Task<UserModel?> GetUserAsync(string username)
        {
            //try
            //{
                string sql = "select * from User where Username = @Username";

                return await dbHelper.QueryScalarAsync<UserModel>(sql, new { Username = username });
            //}
            //catch (Exception e)
            //{
            //    loggingService.Log(e.Message);

            //    throw new GettingUserException();
            //}
        }

        public async Task<int?> GetUserIdAsync(string email)
        {
            //try
            //{
                string sql = "select UserId from UserDetails where Email = @Email";

                return await dbHelper.QueryScalarAsync<int?>(sql,new { Email = email});
            //}
            //catch (Exception e)
            //{
            //    loggingService.Log(e.Message);

            //    throw new GettingUserException();
            //}
        }

    }
}
