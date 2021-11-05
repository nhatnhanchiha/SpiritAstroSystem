using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SpiritAstro.BusinessTier.Generations.Services;
using SpiritAstro.BusinessTier.Requests.Post;
using SpiritAstro.BusinessTier.Responses;
using SpiritAstro.BusinessTier.ViewModels.Post;
using System.Net;
using System.Threading.Tasks;
using SpiritAstro.BusinessTier.Entities;
using SpiritAstro.WebApi.Attributes;

namespace SpiritAstro.WebApi.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IPostService _postService;

        public PostsController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpGet]
        public async Task<IActionResult> GetPosts([FromQuery] PostModel postFilter, [FromQuery] string[] fields, string sort, int page, int limit)
        {
            try
            {
                var posts = await _postService.GetPosts(postFilter, fields, sort, page, limit);
                return Ok(MyResponse<PageResult<PostModel>>.OkWithData(posts));
            }
            catch (ErrorResponse e)
            {
                return Ok(MyResponse<object>.FailWithMessage(e.Error.Message));
            }
        }

        [HttpGet("admin")]
        [CasbinAuthorize]
        public IActionResult GetPostsForAdmin([FromQuery] PostModel postFilter, [FromQuery] string[] fields,
            string sort, int page, int limit)
        {
            try
            {
                var posts = _postService.GetPostsForAdmin(postFilter, fields, sort, page, limit);
                return Ok(MyResponse<PageResult<PostModel>>.OkWithData(posts));
            }
            catch (ErrorResponse e)
            {
                return Ok(MyResponse<object>.FailWithMessage(e.Error.Message));
            }
        }
        
        [HttpGet("astrologer")]
        [CasbinAuthorize]
        public IActionResult GetPostsForAstrologer([FromQuery] PostModel postFilter, [FromQuery] string[] fields,
            string sort, int page, int limit)
        {
            try
            {
                var claims = (CustomClaims)HttpContext.Items["claims"];
                var id = claims!.UserId;
                postFilter.AstrologerId = id;
                var posts = _postService.GetPostsForAdmin(postFilter, fields, sort, page, limit);
                return Ok(MyResponse<PageResult<PostModel>>.OkWithData(posts));
            }
            catch (ErrorResponse e)
            {
                return Ok(MyResponse<object>.FailWithMessage(e.Error.Message));
            }
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
        [CasbinAuthorize]
        public async Task<IActionResult> CreateNewPost(
            [FromBody] CreatePostRequest createPostRequest)
        {
            var claims = (CustomClaims)HttpContext.Items["claims"];

            var id = claims!.UserId;
            try
            {
                var postId = await _postService.CreatePost(createPostRequest, id);
                return Ok(MyResponse<long>.OkWithDetail(postId,
                    $"Created success post with id = {postId}"));
            }
            catch (ErrorResponse e)
            {
                return Ok(MyResponse<object>.FailWithMessage(e.Error.Message));
            }
        }

        [HttpPut("{id:long}")]
        [CasbinAuthorize]
        public async Task<IActionResult> UpdatePost(long id, [FromBody] UpdatePostRequest updatePostRequest)
        {
            try
            {
                var claims = (CustomClaims)HttpContext.Items["claims"];
                var userId = claims!.UserId;
                
                if (!claims.Roles.Split(",").Contains("8888"))
                {
                    userId = 0;
                }
                
                await _postService.UpdatePost(userId, id, updatePostRequest);
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
        [CasbinAuthorize]
        public async Task<IActionResult> DeletePost(long id)
        {
            try
            {
                var claims = (CustomClaims)HttpContext.Items["claims"];
                var userId = claims!.UserId;
                if (!claims.Roles.Split(",").Contains("8888"))
                {
                    userId = 0;
                }
                
                await _postService.DeletePost(userId, id);
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

        [HttpPatch("approve")]
        [CasbinAuthorize]
        public async Task<IActionResult> Approve([FromQuery] long id)
        {
            try
            {
                await _postService.Approve(id);
                return Ok(MyResponse<object>.OkWithMessage("Approved success"));
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