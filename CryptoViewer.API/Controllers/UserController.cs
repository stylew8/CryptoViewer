using System.Net;
using CryptoViewer.Auth_API.Models;
using CryptoViewer.Auth_API.Models.Dto;
using CryptoViewer.Auth_API.Repository.IRepository;
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
