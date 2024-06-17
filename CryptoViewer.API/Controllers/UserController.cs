using System.Net;
using CryptoViewer.API.Models;
using CryptoViewer.Auth_API.Models;
using CryptoViewer.BL.Repositories.Interfaces;
using CryptoViewer.DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CryptoViewer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository repo;
        protected APIResponse response;

        public UserController(IUserRepository repo)
        {
            this.repo = repo;
        }

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


            return BadRequest(APIResponse.CreateBadRequest(new List<string>() { "Was not found UserDetails" }));
        }

        [HttpPost]
        [Route("UpdateUserDetailsById")]
        public async Task<IActionResult> UpdateUserDetailsByIdS([FromBody] UpdateUserDetailsByIdRequest request)
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
                return BadRequest(APIResponse.CreateBadRequest(new List<string>() { $"Problems with database : {e.Message}" }));
            }

            response = new APIResponse();
            response.IsSuccess = true;
            response.StatusCode = HttpStatusCode.OK;

            return Ok(new { response.IsSuccess, response.StatusCode });
            

        }
    }
}
