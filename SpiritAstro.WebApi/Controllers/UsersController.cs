using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using FirebaseAdmin.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpiritAstro.BusinessTier.Generations.Services;
using SpiritAstro.BusinessTier.Requests;
using SpiritAstro.BusinessTier.Requests.User;
using SpiritAstro.BusinessTier.Responses;
using SpiritAstro.BusinessTier.Responses.User;
using SpiritAstro.WebApi.Attributes;

namespace SpiritAstro.WebApi.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [CustomAuthorize]
        [HttpPost("login")]
        public async Task<IActionResult> LoginWithToken([FromBody] LoginRequest loginRequest)
        {
            var decodedToken = await FirebaseAuth.DefaultInstance
                .VerifyIdTokenAsync(loginRequest.Token);
            var uid = decodedToken.Uid;
            
            try
            {
                var loginResponse = await _userService.Login(uid);
                return Ok(MyResponse<LoginResponse>.OkWithData(loginResponse));
            }
            catch (ErrorResponse e)
            {
                if (e.Error.Code == (int)HttpStatusCode.NotFound)
                {
                    return Ok(MyResponse<dynamic>.FailWithDetail(new
                    {
                        isRegister = false
                    }, "User has not registered yet!"));
                }

                return Ok(MyResponse<object>.FailWithMessage(e.Error.Message));
            }
        }
    }
}