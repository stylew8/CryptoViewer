using System.Net;
using CryptoViewer.API.Models;
using CryptoViewer.Auth_API.Models;
using CryptoViewer.BL.Auth.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CryptoViewer.API.Controllers
{
    /// <summary>
    /// Controller handling database session related API endpoints.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class DbSessionController : ControllerBase
    {
        private readonly IDbSession dbSession;
        protected APIResponse response;

        /// <summary>
        /// Initializes a new instance of the <see cref="DbSessionController"/> class.
        /// </summary>
        /// <param name="dbSession">The database session service.</param>
        public DbSessionController(IDbSession dbSession)
        {
            this.dbSession = dbSession;
            response = new APIResponse();
        }

        /// <summary>
        /// Retrieves session ID by user ID.
        /// </summary>
        /// <param name="userId">The request containing the user ID.</param>
        /// <returns>
        /// Returns HTTP 200 OK with session ID if found; otherwise, returns HTTP 400 BadRequest.
        /// </returns>
        [HttpPost("getSessionIdById")]
        [Authorize(Roles = "admin, user")]
        public async Task<IActionResult> GetSessionById([FromBody] GetSessionByIdRequest userId)
        {
            var sessionId = await dbSession.GetSessionId(userId.userId);

            if (sessionId == null)
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                response.IsSuccess = false;
                response.ErrorMessages = new List<string>() { "Session ID not found." };
                return BadRequest(new
                {
                    response.StatusCode,
                    response.IsSuccess,
                    response.ErrorMessages
                });
            }
            response.IsSuccess = true;
            response.StatusCode = HttpStatusCode.OK;
            response.Result = sessionId;

            return Ok(new
            {
                response.IsSuccess,
                response.StatusCode,
                response.Result
            });
        }

        /// <summary>
        /// Checks if a session ID is logged in.
        /// </summary>
        /// <param name="sessionIdString">The request containing the session ID.</param>
        /// <returns>
        /// Returns HTTP 200 OK with true if session is logged in; otherwise, returns HTTP 400 BadRequest with false.
        /// </returns>
        [HttpPost("isLoggedInByGuid")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> IsLoggedIn([FromBody] IsLoggedInRequest sessionIdString)
        {
            var isLoggedIn = await dbSession.IsLoggedIn(sessionIdString.sessionId);

            if (!isLoggedIn)
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                response.IsSuccess = true;
                response.Result = false;
                return Ok(new
                {
                    response.StatusCode,
                    response.IsSuccess,
                    response.Result
                });
            }
            response.IsSuccess = true;
            response.StatusCode = HttpStatusCode.OK;
            response.Result = true;

            return Ok(new
            {
                response.IsSuccess,
                response.StatusCode,
                response.Result
            });
        }
    }
}