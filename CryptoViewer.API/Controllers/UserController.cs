using System.Net;
using CryptoViewer.Auth_API.Models;
using CryptoViewer.Auth_API.Models.Dto;
using CryptoViewer.Auth_API.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace CryptoViewer.API.Controllers
{
    [Route("api/UsersAuth")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository userRepo;
        protected APIResponse response; 

        public UserController(IUserRepository userRepo)
        {
            this.userRepo = userRepo;
            response = new APIResponse();
        }
        /// <summary>
        /// Authenticates a user and returns a login response with a token.
        /// </summary>
        /// <remarks>
        /// This method allows a user to log in by providing their credentials. If the login is successful, it returns a JWT token that can be used for authenticated requests.
        /// </remarks>
        /// <param name="model">The login request data transfer object containing username and password.</param>
        /// <response code="200">Returns a login response containing the user details and authentication token</response>
        /// <response code="400">Returns an error message indicating that the username or password is incorrect</response>
        /// <response code="500">Returns a general error message indicating an issue with the server</response>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto model)
        {
            var loginResponse = await userRepo.Login(model);

            if (loginResponse.User == null || string.IsNullOrEmpty(loginResponse.Token))
            {
                response.ErrorMessages.Add("Username or password is incorrect");
                response.IsSuccess = false;
                response.StatusCode = HttpStatusCode.BadRequest;
                return BadRequest(new{response.IsSuccess, response.StatusCode, response.ErrorMessages});
            }

            response.IsSuccess = true;
            response.StatusCode = HttpStatusCode.OK;
            response.Result = loginResponse;

            return Ok(new{ response.IsSuccess, response.StatusCode, response.Result});
        }
        /// <summary>
        /// Registers a new user with the provided registration details.
        /// </summary>
        /// <remarks>
        /// This method allows a new user to register by providing their details. If the registration is successful, a confirmation is returned. 
        /// If the username already exists, or if there is an error during registration, appropriate error messages are returned.
        /// </remarks>
        /// <param name="model">The registration request data transfer object containing user details.</param>
        /// <response code="200">User registered successfully.</response>
        /// <response code="400">Username already exists or there was an error while registering.</response>
        /// <response code="500">Oops! Can't register the user right now.</response>
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto model)
        {
            bool ifUsernameUnique = userRepo.isUniqueUser(model.Username);

            if (!ifUsernameUnique)
            {

                response.IsSuccess = false;
                response.StatusCode = HttpStatusCode.BadRequest;
                response.ErrorMessages.Add("Username already exists");
                return BadRequest(new { response.IsSuccess , response.ErrorMessages, response.StatusCode});
            }

            var user = await userRepo.Register(model);

            if (user == null)
            {
                response.IsSuccess = false;
                response.StatusCode = HttpStatusCode.BadRequest;
                response.ErrorMessages.Add("Error while registering");
                return BadRequest(new { response.IsSuccess, response.StatusCode, response.ErrorMessages });
            }

            response.IsSuccess = true;
            response.StatusCode = HttpStatusCode.OK;
            return Ok(new {response.IsSuccess, response.StatusCode});
        }
    }
}
