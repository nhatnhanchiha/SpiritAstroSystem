
using Microsoft.AspNetCore.Mvc;
using SpiritAstro.BusinessTier.Generations.Services;
using SpiritAstro.BusinessTier.Requests.PostZodiac;
using SpiritAstro.BusinessTier.Responses;
using SpiritAstro.BusinessTier.ViewModels.PostZodiac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace SpiritAstro.WebApi.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class PostZodiacController : ControllerBase
    {
        private readonly IPostZodiacService _postZodiacService;

        public PostZodiacController(IPostZodiacService postZodiacService)
        {
            _postZodiacService = postZodiacService;
        }

        [HttpPost]
        public async Task<IActionResult> CreatePostZodiacCategory([FromBody] CreatePostZodiacRequest createPostZodiac)
        {
            try
            {
                var postId = await _postZodiacService.CreatePostZodiac(createPostZodiac);
                return Ok(MyResponse<long>.OkWithDetail(postId,
                    $"Created success post_zodiac with postId = {postId}"));
            }
            catch (ErrorResponse e)
            {
                return Ok(MyResponse<object>.FailWithMessage(e.Error.Message));
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetListPostZodiac([FromQuery] PostZodiacModel postZodiacFilter, [FromQuery] string[] fields, string sort, int page, int limit)
        {
            try
            {
                var pzModels = await _postZodiacService.GetListPostZodiac(postZodiacFilter, page, limit, sort, fields);
                return Ok(MyResponse<PageResult<PostZodiacModel>>.OkWithData(pzModels));
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
