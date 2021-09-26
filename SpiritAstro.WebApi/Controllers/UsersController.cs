using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SpiritAstro.BusinessTier.Entities;
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

        [HttpGet]
        public async Task<IActionResult> GetListUser([FromQuery] UserModel userFilter, int page, int limit)
        {
            try
            {
                var userModels = await _userService.GetListUser(userFilter, page, limit);
                return Ok(MyResponse<PageResult<UserModel>>.OkWithData(userModels));
            }
            catch (ErrorResponse e)
            {
                return e.Error.Code switch
                {
                    _ => Ok(MyResponse<object>.FailWithMessage(e.Error.Message))
                };
            }
        }

        [HttpGet("{id:long}")]
        public async Task<IActionResult> GerDetailUser(long id)
        {
            var claims = (CustomClaims) HttpContext.Items["claims"];
            
            try
            {
                if (claims == null || claims.UserId != id)
                {
                    var publicUserModel = await _userService.GetPublicDetailOfUserById(id);
                    return Ok(MyResponse<PublicUserModel>.OkWithData(publicUserModel));
                }
                var userModel = await _userService.GetDetailUser(id);
                return Ok(MyResponse<UserModel>.OkWithData(userModel));
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

        [HttpPut("{id:long}")]
        public async Task<IActionResult> UpdateUser(long id, [FromBody] UpdateUserRequest user)
        {
            var claims = (CustomClaims)HttpContext.Items["claims"];
            try
            {
                if (claims == null || claims.UserId != id)
                {
                    return Ok(MyResponse<object>.FailWithMessage("Access denied !"));
                }

                await _userService.UpdateUser(id, user);
                return Ok(MyResponse<object>.OkWithMessage("Updated success"));
            }
            catch (ErrorResponse e)
            {
                if (e.Error.Code == (int)HttpStatusCode.NotFound)
                {
                    return Ok(MyResponse<object>.FailWithMessage("Updated fail. " + e.Error.Message));
                }

                return Ok(MyResponse<object>.FailWithMessage("Updated fail. " + e.Error.Message));
            }
        }

        [HttpDelete("{id:long}")]
        public async Task<IActionResult> DeleteUser(long id)
        {
            var claims = (CustomClaims)HttpContext.Items["claims"];
            try
            {
                if (claims == null)
                {
                    return Ok(MyResponse<object>.FailWithMessage("Access denied !"));
                }
                await _userService.DeleteUser(id);
                return Ok(MyResponse<object>.OkWithMessage("Deleted success"));
            }
            catch (ErrorResponse e)
            {
                if (e.Error.Code == (int)HttpStatusCode.NotFound)
                {
                    return Ok(MyResponse<object>.FailWithMessage("Deleted fail. " + e.Error.Message));
                }

                return Ok(MyResponse<object>.FailWithMessage("Deleted fail. " + e.Error.Message));
            }
        }
    }
}