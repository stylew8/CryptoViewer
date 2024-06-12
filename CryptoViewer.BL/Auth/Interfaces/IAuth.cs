using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CryptoViewer.DAL.Models;

namespace CryptoViewer.BL.Auth.Interfaces
{
    public interface IAuth
    {
        Task<int> CreateUser(UserModel model, UserDetails details);
        Task<int> Authenticate(string email, string password);
        Task ValidateEmail(string email);
        Task ValidateUsername(string username);
        Task<int> Register(UserModel user, UserDetails details);
    }
}
