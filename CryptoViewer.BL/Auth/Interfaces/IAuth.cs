using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CryptoViewer.DAL.Models;

namespace CryptoViewer.BL.Auth.Interfaces
{
    /// <summary>
    /// Interface defining authentication and user management operations.
    /// </summary>
    public interface IAuth
    {
        /// <summary>
        /// Creates a new user with the provided model and details.
        /// </summary>
        /// <param name="model">The user model containing basic user information.</param>
        /// <param name="details">The details model containing additional user details.</param>
        /// <returns>A task that represents the asynchronous operation, returning the ID of the created user.</returns>
        Task<int> CreateUser(UserModel model, UserDetails details);

        /// <summary>
        /// Authenticates a user with the provided email and password.
        /// </summary>
        /// <param name="email">The email of the user to authenticate.</param>
        /// <param name="password">The password of the user.</param>
        /// <returns>A task that represents the asynchronous operation, returning the ID of the authenticated user.</returns>
        Task<int> Authenticate(string email, string password);

        /// <summary>
        /// Validates the uniqueness of the provided email.
        /// </summary>
        /// <param name="email">The email to validate.</param>
        /// <returns>A task that represents the asynchronous operation for validating the email.</returns>
        Task ValidateEmail(string email);

        /// <summary>
        /// Validates the uniqueness of the provided username.
        /// </summary>
        /// <param name="username">The username to validate.</param>
        /// <returns>A task that represents the asynchronous operation for validating the username.</returns>
        Task ValidateUsername(string username);

        /// <summary>
        /// Registers a new user with the provided user model and details.
        /// </summary>
        /// <param name="user">The user model containing user information.</param>
        /// <param name="details">The details model containing additional user details.</param>
        /// <returns>A task that represents the asynchronous operation, returning the ID of the registered user.</returns>
        Task<int> Register(UserModel user, UserDetails details);
    }
}