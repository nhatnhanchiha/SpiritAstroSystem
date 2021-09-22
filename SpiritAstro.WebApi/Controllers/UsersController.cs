using System.Net;
using Microsoft.AspNetCore.Mvc;
using SpiritAstro.BusinessTier.Generations.Services;
using SpiritAstro.BusinessTier.Requests.User;
using SpiritAstro.BusinessTier.Responses;
using SpiritAstro.BusinessTier.Responses.User;

namespace SpiritAstro.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("login")]
        public IActionResult LoginByPhone([FromBody] LoginRequest loginRequest)
        {
            try
            {
                var loginResponse = _userService.LoginByPhone(loginRequest);
                return Ok(MyResponse<LoginResponse>.OkWithDetail(loginResponse, "Login success"));
            }
            catch (ErrorResponse e)
            {
                if (e.Error.Code == (int)HttpStatusCode.NotFound)
                {
                    return Ok(MyResponse<object>.FailWithMessage(e.Error.Message));
                }
                
                return Ok(MyResponse<object>.FailWithMessage("Not handler error"));
            }
        }
    }
}