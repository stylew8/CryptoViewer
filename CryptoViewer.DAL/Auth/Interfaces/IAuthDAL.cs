using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CryptoViewer.DAL.Models;

namespace CryptoViewer.DAL.Auth.Interfaces
{
    public interface IAuthDAL
    {
        Task<int?> CreateUserAsync(UserModel model);
        Task<int?> CreateUserDetailsAsync(UserDetails model);
        Task<UserModel?> GetUserAsync(int id);
        Task<int?> GetUserIdAsync(string email);
        Task<UserModel?> GetUserAsync(string username);
    }
}
