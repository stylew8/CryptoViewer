using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CryptoViewer.API.Authorization.Models;
using CryptoViewer.Auth_API.Models.Dto;

namespace CryptoViewer.Auth_API.Repository.IRepository
{
    /// <summary>
    /// Interface defining user repository operations.
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Checks if the given username is unique.
        /// </summary>
        /// <param name="username">The username to check.</param>
        /// <returns>True if the username is unique; false otherwise.</returns>
        bool isUniqueUser(string username);

        /// <summary>
        /// Performs user login with the provided credentials.
        /// </summary>
        /// <param name="loginRequestDto">The login request DTO containing username and password.</param>
        /// <returns>A task that represents the asynchronous login operation, returning a <see cref="LoginResponseDto"/>.</returns>
        Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto);

        /// <summary>
        /// Registers a new user with the provided information.
        /// </summary>
        /// <param name="registerRequestDto">The register request DTO containing user details.</param>
        /// <returns>A task that represents the asynchronous registration operation, returning the registered <see cref="LocalUser"/>.</returns>
        Task<LocalUser> Register(RegisterRequestDto registerRequestDto);
    }
}