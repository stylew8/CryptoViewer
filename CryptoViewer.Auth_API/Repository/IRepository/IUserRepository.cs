using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CryptoViewer.API.Authorization.Models;
using CryptoViewer.Auth_API.Models.Dto;

namespace CryptoViewer.Auth_API.Repository.IRepository
{
    public interface IUserRepository
    {
        bool isUniqueUser(string username);
        Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto);

        Task<LocalUser> Register(RegisterRequestDto registerRequestDto);
    }
}
