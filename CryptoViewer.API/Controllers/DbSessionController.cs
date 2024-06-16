using System.Net;
using CryptoViewer.API.Models;
using CryptoViewer.Auth_API.Models;
using CryptoViewer.BL.Auth.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CryptoViewer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DbSessionController : ControllerBase
    {
        private readonly IDbSession dbSession;
        protected APIResponse response;

        public DbSessionController(IDbSession dbSession)
        {
            this.dbSession = dbSession;
            response = new APIResponse();
        }

        [HttpPost("getSessionIdById")]
        [Authorize(Roles = "admin, user")]
        public async Task<IActionResult> GetSessionById([FromBody] GetSessionByIdRequest userId)
        {
            var sessionId = await dbSession.GetSessionId(userId.userId);

            if (sessionId == null)
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                response.IsSuccess = false;
                response.ErrorMessages = new List<string>(){"Session Id is not found"};
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

        [HttpPost("isLoggedInByGuid")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> IsLoggedIn([FromBody]IsLoggedInRequest sessionIdString)
        {
            var sessionId = await dbSession.IsLoggedIn(sessionIdString.sessionId);

            if (!sessionId)
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
