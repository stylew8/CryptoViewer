using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using CryptoViewer.API.Authorization.Models;
using CryptoViewer.Auth_API.Models;
using CryptoViewer.Auth_API.Models.Dto;
using CryptoViewer.Auth_API.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Org.BouncyCastle.Bcpg;

namespace CryptoViewer.Auth_API.Repository
{
    /// <summary>
    /// Repository class responsible for user-related operations.
    /// </summary>
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext db;
        private readonly string secretKey;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserRepository"/> class.
        /// </summary>
        /// <param name="db">The database context instance.</param>
        /// <param name="apiSettings">The API settings.</param>
        public UserRepository(AppDbContext db, IOptions<ApiSettings> apiSettings)
        {
            this.db = db;
            secretKey = apiSettings.Value.Secret ?? throw new ArgumentNullException("API secret key is not configured.");
        }

        /// <summary>
        /// Checks if the given username is unique in the database.
        /// </summary>
        /// <param name="username">The username to check.</param>
        /// <returns>True if the username is unique; false otherwise.</returns>
        public bool isUniqueUser(string username)
        {
            var user = db.LocalUsers.FirstOrDefault(x => x.Username == username);
            if (user == null)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Attempts to log in a user with the provided credentials.
        /// </summary>
        /// <param name="loginRequestDto">The login request DTO containing username and password.</param>
        /// <returns>A <see cref="LoginResponseDto"/> containing login result and JWT token.</returns>
        public async Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto)
        {
            var user = await db.LocalUsers.FirstOrDefaultAsync(u =>
                                                                u.Username.ToLower() == loginRequestDto.Username.ToLower()
                                                              && u.Password == loginRequestDto.Password
                                                                );
            if (user == null)
            {
                return new LoginResponseDto()
                {
                    Token = "",
                    User = null
                };
            }

            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(secretKey);
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name,user.Id.ToString()),
                    new Claim(ClaimTypes.Role,user.Role)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var loginResponseDto = new LoginResponseDto()
            {
                Token = tokenHandler.WriteToken(token),
                User = user
            };

            user.Password = "";
            return loginResponseDto;
        }

        /// <summary>
        /// Registers a new user with the provided information.
        /// </summary>
        /// <param name="registerRequestDto">The register request DTO containing user details.</param>
        /// <returns>The registered <see cref="LocalUser"/> entity.</returns>
        public async Task<LocalUser> Register(RegisterRequestDto registerRequestDto)
        {
            LocalUser user = new LocalUser()
            {
                Username = registerRequestDto.Username,
                Name = registerRequestDto.Name,
                Password = registerRequestDto.Password,
                Role = registerRequestDto.Role
            };

            db.LocalUsers.Add(user);
            await db.SaveChangesAsync();

            user.Password = "";
            return user;
        }
    }
}