using CryptoViewer.API.Models;
using CryptoViewer.BL.Auth.Interfaces;
using CryptoViewer.BL.Exceptions;
using CryptoViewer.DAL.Exceptions;
using CryptoViewer.DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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

        [HttpPost("login")]
        [Authorize(Roles = "admin")]
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

        [HttpPost("register")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            //try
            //{
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
                    return BadRequest("Registration failed.");
                }
            //}
            //catch (DuplicateEmailException)
            //{
            //    return BadRequest("Email already exist.");
            //}
            //catch (DuplicateUsernameException)
            //{
            //    return BadRequest("Username already exist.");
            //}
            //catch (CreatingUserDetailsException)
            //{
            //    return BadRequest("UserDetails creating is failed.");
            //}
            //catch (CreatingUserException)
            //{
            //    return BadRequest("User creating is failed.");
            //}
            //catch
            //{
            //    return BadRequest("Error during registration.");
            //}
        }

    }
}
