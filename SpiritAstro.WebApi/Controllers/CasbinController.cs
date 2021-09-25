using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpiritAstro.BusinessTier.Requests.Casbin;
using SpiritAstro.BusinessTier.Responses;
using SpiritAstro.BusinessTier.Services;
using SpiritAstro.WebApi.Attributes;

namespace SpiritAstro.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CasbinController : ControllerBase
    {
        private readonly ICasbinService _casbinService;

        public CasbinController(ICasbinService casbinService)
        {
            _casbinService = casbinService;
        }

        [HttpGet]
        [CasbinAuthorize]
        public IActionResult GetAllPermissionsOfRole([FromQuery] GetAllPermissionsRequest getAllPermissionsRequest)
        {
            try
            {
                return Ok(MyResponse<List<List<string>>>.OkWithData(_casbinService.GetAllPermissions(getAllPermissionsRequest.Role)));
            }
            catch (ErrorResponse e)
            {
                return e.Error.Code switch
                {
                    _ => Ok(MyResponse<object>.FailWithMessage(e.Error.Message))
                };
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreatePolicy([FromBody] AddPolicyRequest addPolicyRequest)
        {
            try
            {
                await _casbinService.AddPermission(addPolicyRequest);
                return Ok(MyResponse<object>.OkWithMessage("Created success"));
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
        public async Task<IActionResult> RemovePolicy([FromBody] RemovePolicyRequest removePolicyRequest)
        {
            try
            {
                await _casbinService.RemovePermission(removePolicyRequest);
                return Ok(MyResponse<object>.OkWithMessage("Deleted success"));
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