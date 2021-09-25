using Microsoft.AspNetCore.Mvc;
using SpiritAstro.BusinessTier.Generations.Services;
using SpiritAstro.BusinessTier.Requests.Post;
using SpiritAstro.BusinessTier.Responses;
using SpiritAstro.BusinessTier.ViewModels.Post;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace SpiritAstro.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IPostService _postService;

        public PostsController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetPostById(long id)
        {
            try
            {
                var postModel = await _postService.GetPostById(id);
                return Ok(MyResponse<PostModel>.OkWithData(postModel));
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

        [HttpPost]
        public async Task<IActionResult> CreateNewPost(
            [FromBody] CreatePostRequest createPostRequest)
        {
            try
            {
                var postId = await _postService.CreatePost(createPostRequest);
                return Ok(MyResponse<long>.OkWithDetail(postId,
                    $"Created success post with id = {postId}"));
            }
            catch (ErrorResponse e)
            {
                return Ok(MyResponse<object>.FailWithMessage(e.Error.Message));
            }
        }

        [HttpPut("{id:long}")]
        public async Task<IActionResult> UpdatePost(long id, [FromBody] UpdatePostRequest updatePostRequest)
        {
            try
            {
                await _postService.UpdatePost(id, updatePostRequest);
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
        public async Task<IActionResult> DeletePost(long id)
        {
            try
            {
                await _postService.DeletePost(id);
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