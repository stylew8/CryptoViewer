using CryptoViewer.API.Models;
using CryptoViewer.BL.Auth.Interfaces;
using CryptoViewer.BL.Exceptions;
using CryptoViewer.DAL.Exceptions;
using CryptoViewer.DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mysqlx;

namespace CryptoViewer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuth _authService;

        public AuthController(IAuth authService)
        {
            _authService = authService;
        }
        /// <summary>
        /// Authenticates an admin user and returns their user ID.
        /// </summary>
        /// <remarks>
        /// This method allows an admin user to log in by providing their credentials. If the login is successful, it returns the user's ID.
        /// </remarks>
        /// <param name="model">The login request data transfer object containing username and password.</param>
        /// <response code="200">User authenticated successfully, returns user ID.</response>
        /// <response code="400">Invalid login attempt.</response>
        /// <response code="500">An error occurred while processing the login request.</response>
        [HttpPost("login")]
        [Authorize(Roles = "admin, user")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            try
            {
                var userId = await _authService.Authenticate(model.Username, model.Password);
                if (userId > 0)
                {
                    return Ok(new { UserId = userId });
                }
                return BadRequest();
            }
            catch
            {
                return BadRequest("Invalid login attempt.");
            }
        }
        /// <summary>
        /// Registers a new user with the provided registration details.
        /// </summary>
        /// <remarks>
        /// This method allows an admin to register a new user by providing their details. If the registration is successful, a confirmation with the user ID is returned. 
        /// If there is an error during registration, appropriate error messages are returned.
        /// </remarks>
        /// <param name="model">The registration request data transfer object containing user details.</param>
        /// <response code="200">User registered successfully, returns user ID.</response>
        /// <response code="400">Registration failed due to invalid input or duplication.</response>
        /// <response code="500">An error occurred while processing the registration request.</response>
        [HttpPost("register")]
        [Authorize(Roles = "admin, user")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            try
            {
                var userId = await _authService.Register(
                    new UserModel
                    {
                        Username = model.Username,
                        Password = model.Password,
                    },
                    new UserDetails()
                    {
                        Email = model.Email,
                        Address = model.Address,
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                    }
                );


                if (userId > 0)
                {
                    return Ok(new { UserId = userId });
                }
                else
                {
                    return BadRequest(new RegisterResponseDto() { Error = "Registration failed." });
                }
            }
            catch (DuplicateEmailException)
            {
                return BadRequest(new RegisterResponseDto() { Error = "Email already exist." });
            }
            catch (DuplicateUsernameException)
            {
                return BadRequest(new RegisterResponseDto() { Error = "Username already exist." });
            }
            catch (CreatingUserDetailsException)
            {
                return BadRequest(new RegisterResponseDto() { Error = "UserDetails creating is failed."});
            }
            catch (CreatingUserException)
            {
                return BadRequest(new RegisterResponseDto() { Error = "User creating is failed." });
            }
            catch
            {
                return BadRequest(new RegisterResponseDto() { Error = "Error during registration." });
            }
        }

    }
}
