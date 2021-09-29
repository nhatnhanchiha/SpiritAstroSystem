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

        [HttpPost]
        public async Task<IActionResult> Follow([FromBody] FollowRequest followRequest)
        {
            var claims = (CustomClaims)HttpContext.Items["claims"];
            try
            {
                await _followService.Follow(followRequest.AstrologerId);
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
        public async Task<IActionResult> GetFollowing(int page, int limit)
        {
            var claims = (CustomClaims)HttpContext.Items["claims"];

            try
            {
                var follows = await _followService.GetFollowings(page, limit);
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
        public async Task<IActionResult> GetFollower(int page, int limit)
        {
            var claims = (CustomClaims)HttpContext.Items["claims"];

            try
            {
                var follows = await _followService.GetFollowers(page, limit);
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