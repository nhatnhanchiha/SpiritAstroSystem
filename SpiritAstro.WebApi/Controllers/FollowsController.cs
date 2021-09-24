using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpiritAstro.BusinessTier.Entities;
using SpiritAstro.BusinessTier.Generations.Services;
using SpiritAstro.BusinessTier.Requests.Follow;
using SpiritAstro.BusinessTier.Responses;
using SpiritAstro.BusinessTier.ViewModels.Follow;
using SpiritAstro.WebApi.Attributes;

namespace SpiritAstro.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FollowsController : ControllerBase
    {
        private readonly IFollowService _followService;
        private readonly IUserService _userService;

        public FollowsController(IFollowService followService, IUserService userService)
        {
            _followService = followService;
            _userService = userService;
        }

        [HttpPost]
        [CasbinAuthorize]
        public async Task<IActionResult> Follow([FromBody] FollowRequest followRequest)
        {
            var claims = (CustomClaims)HttpContext.Items["claims"];
            try
            {
                await _userService.IsAstrologer(followRequest.AstrologerId);
                await _followService.Follow(claims!.UserId, followRequest.AstrologerId);
                return Ok(MyResponse<object>.OkWithMessage("Followed success"));
            }
            catch (ErrorResponse e)
            {
                return e.Error.Code switch
                {
                    (int)HttpStatusCode.NotFound => Ok(MyResponse<object>.FailWithMessage(e.Error.Message)),
                    (int)HttpStatusCode.BadRequest => Ok(MyResponse<object>.FailWithMessage(e.Error.Message)),
                    _ => Ok(MyResponse<object>.FailWithMessage(e.Error.Message))
                };
            }
        }

        [HttpGet("getFollowings")]
        [CasbinAuthorize]
        public async Task<IActionResult> GetFollowing(int page, int limit)
        {
            var claims = (CustomClaims)HttpContext.Items["claims"];

            try
            {
                var follows = await _followService.GetFollowings(claims!.UserId, page, limit);
                return Ok(MyResponse<PageResult<FollowWithAstrologer>>.OkWithData(follows));
            }
            catch (ErrorResponse e)
            {
                return e.Error.Code switch
                {
                    _ => Ok(MyResponse<object>.FailWithMessage(e.Error.Message))
                };
            }
        }

        [HttpGet("getFollowers")]
        public async Task<IActionResult> GetFollower(int page, int limit)
        {
            var claims = (CustomClaims)HttpContext.Items["claims"];

            try
            {
                var follows = await _followService.GetFollowers(claims!.UserId, page, limit);
                return Ok(MyResponse<PageResult<FollowWithCustomer>>.OkWithData(follows));
            }
            catch (ErrorResponse e)
            {
                return e.Error.Code switch
                {
                    _ => Ok(MyResponse<object>.FailWithMessage(e.Error.Message))
                };
            }
        }
    }
}