using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using CryptoViewer.API.Models;
using CryptoViewer.Auth_API.Models;
using CryptoViewer.BL.Repositories.Interfaces;
using CryptoViewer.DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CryptoViewer.API.Controllers
{
    /// <summary>
    /// Controller handling user-related API endpoints.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository repo;
        protected APIResponse response;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserController"/> class.
        /// </summary>
        /// <param name="repo">The repository for user-related operations.</param>
        public UserController(IUserRepository repo)
        {
            this.repo = repo;
        }

        /// <summary>
        /// Retrieves user details by session ID.
        /// </summary>
        /// <param name="request">The request containing the session ID.</param>
        /// <returns>
        /// Returns HTTP 200 OK with user details if successful; otherwise, returns HTTP 400 BadRequest.
        /// </returns>
        [HttpPost]
        [Route("UserDetailsBySessionId")]
        public async Task<IActionResult> GetUserDetailsBySessionId([FromBody] GetUserDetailsBySessionIdRequest request)
        {
            var x = await repo.GetUserDetailsBySessionId(request.SessionId);

            response = new APIResponse();

            if (x != null)
            {
                response.IsSuccess = true;
                response.StatusCode = HttpStatusCode.OK;
                response.Result = x;

                return Ok(new { response.IsSuccess, response.StatusCode, response.Result });
            }

            return BadRequest(APIResponse.CreateBadRequest(new List<string>() { "User details not found." }));
        }

        /// <summary>
        /// Updates user details by ID.
        /// </summary>
        /// <param name="request">The request containing the updated user details.</param>
        /// <returns>
        /// Returns HTTP 200 OK if the update is successful; otherwise, returns HTTP 400 BadRequest.
        /// </returns>
        [HttpPost]
        [Route("UpdateUserDetailsById")]
        public async Task<IActionResult> UpdateUserDetailsById([FromBody] UpdateUserDetailsByIdRequest request)
        {
            var model = new UserDetails()
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Address = request.Address,
                ModifiedAt = DateTime.UtcNow,
                Id = request.Id
            };

            try
            {
                await repo.UpdateUserDetails(model);
            }
            catch (Exception e)
            {
                return BadRequest(APIResponse.CreateBadRequest(new List<string>() { $"Database error: {e.Message}" }));
            }

            response = new APIResponse();
            response.IsSuccess = true;
            response.StatusCode = HttpStatusCode.OK;

            return Ok(new { response.IsSuccess, response.StatusCode });
        }
    }
}