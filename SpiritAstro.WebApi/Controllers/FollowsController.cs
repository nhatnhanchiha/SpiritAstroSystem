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
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class FollowsController : ControllerBase
    {
        private readonly IFollowService _followService;

        public FollowsController(IFollowService followService)
        {
            _followService = followService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] FollowModel filter, [FromQuery] string[] fields, string sort, int page, int limit)
        {
            try
            {
                var followList = await _followService.GetFollowList(filter, fields, sort, page, limit);
                return Ok(MyResponse<PageResult<FollowModel>>.OkWithData(followList));
            }
            catch (ErrorResponse e)
            {
                return Ok(MyResponse<object>.FailWithMessage(e.Error.Message));
            }
        }

        [HttpPost]
        [CasbinAuthorize]
        public async Task<IActionResult> Follow([FromBody] FollowRequest followRequest)
        {
            var claims = (CustomClaims)HttpContext.Items["claims"];

            var customerId = claims!.UserId;
            try
            {
                await _followService.Follow(customerId, followRequest.AstrologerId);
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

        [HttpGet("followings")]
        [CasbinAuthorize]
        public async Task<IActionResult> GetFollowing(int page, int limit)
        {
            var claims = (CustomClaims)HttpContext.Items["claims"];
            var customerId = claims!.UserId;

            try
            {
                var follows = await _followService.GetFollowings(customerId, page, limit);
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

        [HttpGet("followers")]
        [CasbinAuthorize]
        public async Task<IActionResult> GetFollower(int page, int limit)
        {
            var claims = (CustomClaims)HttpContext.Items["claims"];
            var astrologerId = claims!.UserId;

            try
            {
                var follows = await _followService.GetFollowers(astrologerId, page, limit);
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

        [HttpDelete]
        [CasbinAuthorize]
        public async Task<IActionResult> Unfollow([FromBody] UnfollowRequest unfollowRequest)
        {
            var claims = (CustomClaims)HttpContext.Items["claims"];
            var customerId = claims!.UserId;

            try
            {
                await _followService.Unfollow(customerId, unfollowRequest.AstrologerId);
                return Ok(MyResponse<object>.OkWithMessage("Unfollow success"));
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
    }
}