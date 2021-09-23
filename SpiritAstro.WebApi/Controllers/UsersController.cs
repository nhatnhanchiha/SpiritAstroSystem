using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SpiritAstro.BusinessTier.Generations.Services;
using SpiritAstro.BusinessTier.Requests.User;
using SpiritAstro.BusinessTier.Responses;
using SpiritAstro.BusinessTier.Responses.User;
using SpiritAstro.BusinessTier.ViewModels.Users;

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

        [HttpPost]
        public async Task<IActionResult> RegisterCustomer([FromBody] RegisterCustomerRequest registerCustomerRequest)
        {
            try
            {
                var userId = await _userService.RegisterCustomer(registerCustomerRequest);
                return Ok(MyResponse<long>.OkWithDetail(userId, $"Created success user with id = {userId}"));
            }
            catch (ErrorResponse e)
            {
                return Ok(MyResponse<object>.FailWithMessage( e.Error.Message));
            }
        }

        [HttpGet("{id:long}")]
        public async Task<IActionResult> GerDetailUser(long id)
        {
            try
            {
                var userModel = await _userService.GetDetailUser(id);
                return Ok(MyResponse<UserModels>.OkWithData(userModel));
            }
            catch (ErrorResponse e)
            {
                if (e.Error.Code == (int)HttpStatusCode.NotFound)
                {
                    return Ok(MyResponse<object>.FailWithMessage(e.Error.Message));
                }

                return Ok(MyResponse<object>.FailWithMessage(e.Error.Message));
            }
        }
    }
}