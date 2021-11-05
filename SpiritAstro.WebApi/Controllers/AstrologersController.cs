using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SpiritAstro.BusinessTier.Entities;
using SpiritAstro.BusinessTier.Generations.Services;
using SpiritAstro.BusinessTier.Requests.Astrologer;
using SpiritAstro.BusinessTier.Responses;
using SpiritAstro.BusinessTier.ViewModels.Astrologer;
using SpiritAstro.WebApi.Attributes;

namespace SpiritAstro.WebApi.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class AstrologersController : ControllerBase
    {
        private readonly IAstrologerService _astrologerService;
        private readonly IUserRoleService _userRoleService;

        public AstrologersController(IAstrologerService astrologerService, IUserRoleService userRoleService)
        {
            _astrologerService = astrologerService;
            _userRoleService = userRoleService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAstrologers([FromQuery] PublicAstrologerModel filter,
            [FromQuery] string[] fields, string sort, int page, int limit)
        {
            try
            {
                var astrologers = await _astrologerService.GetAllAstrologers(filter, fields, sort, page, limit);
                return Ok(MyResponse<PageResult<PublicAstrologerModel>>.OkWithData(astrologers));
            }
            catch (ErrorResponse e)
            {
                return Ok(MyResponse<object>.FailWithMessage(e.Error.Message));
            }
        }
        
        [HttpGet("admin")]
        public async Task<IActionResult> GetAllAstrologersForAdmin([FromQuery] PublicAstrologerModelForAdmin filter,
            [FromQuery] string[] fields, string sort, int page, int limit)
        {
            try
            {
                var astrologers = await _astrologerService.GetAllAstrologersForAdmin(filter, fields, sort, page, limit);
                return Ok(MyResponse<PageResult<PublicAstrologerModelForAdmin>>.OkWithData(astrologers));
            }
            catch (ErrorResponse e)
            {
                return Ok(MyResponse<object>.FailWithMessage(e.Error.Message));
            }
        }

        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetAstrologerById(long id)
        {
            var claims = (CustomClaims)HttpContext.Items["claims"];

            try
            {
                if (claims == null || claims.UserId != id)
                {
                    var publicAstrologerModel = await _astrologerService.GetPublicAstrologerById(id);
                    return Ok(MyResponse<PublicAstrologerModel>.OkWithData(publicAstrologerModel));
                }

                var astrologerModel = await _astrologerService.GetAstrologerById(id);
                return Ok(MyResponse<AstrologerModel>.OkWithData(astrologerModel));
            }
            catch (ErrorResponse e)
            {
                return e.Error.Code switch
                {
                    (int)HttpStatusCode.NotFound => Ok(MyResponse<object>.FailWithMessage(e.Error.Message)),
                    _ => Ok(MyResponse<object>.FailWithMessage(e.Error.Message))
                };
            }
        }

        [HttpPost]
        [CasbinAuthorize]
        public async Task<IActionResult> RegisterAnAstrologer([FromBody] RegisterAstrologerRequest registerAstrologerRequest)
        {
            try
            {
                await _astrologerService.RegisterAnAstrologer(registerAstrologerRequest);
                // Todo: set user role for this astrologer
                return Ok(MyResponse<object>.OkWithMessage("Registered success"));
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

        [HttpPut("{id:long}")]
        [CasbinAuthorize]
        public async Task<IActionResult> UpdateAnAstrologer(long id, [FromBody] UpdateAstrologerRequest updateAstrologerRequest)
        {
            var claims = (CustomClaims)HttpContext.Items["claims"];

            if (claims!.UserId != id)
            {
                return Ok(MyResponse<object>.FailWithMessage("Insufficient permissions"));
            }

            try
            {
                await _astrologerService.UpdateAnAstrologer(id, updateAstrologerRequest);
                return Ok(MyResponse<object>.OkWithMessage("Updated success"));
            }
            catch (ErrorResponse e)
            {
                return e.Error.Code switch
                {
                    (int)HttpStatusCode.NotFound => Ok(MyResponse<object>.FailWithMessage(e.Error.Message)),
                    _ => Ok(MyResponse<object>.FailWithMessage(e.Error.Message))
                };
            }
        }

        [HttpDelete("{id:long}")]
        [CasbinAuthorize]
        public async Task<IActionResult> DeleteAnAstrologer(long id)
        {
            try
            {
                await _astrologerService.DeleteAnAstrologer(id);
                // Todo: Delete user role for this astrologer
                return Ok(MyResponse<object>.OkWithMessage("Deleted success"));
            }
            catch (ErrorResponse e)
            {
                return e.Error.Code switch
                {
                    (int)HttpStatusCode.NotFound => Ok(MyResponse<object>.FailWithMessage(e.Error.Message)),
                    _ => Ok(MyResponse<object>.FailWithMessage(e.Error.Message))
                };
            }
        }
    }
}