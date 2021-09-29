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
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class CasbinController : ControllerBase
    {
        private readonly ICasbinService _casbinService;

        public CasbinController(ICasbinService casbinService)
        {
            _casbinService = casbinService;
        }

        [HttpPost]
        [CasbinAuthorize]
        public async Task<IActionResult> AddPolicy([FromBody] AddPolicyRequest addPolicyRequest)
        {
            try
            {
                await _casbinService.AddPolicy(addPolicyRequest);
                return Ok(MyResponse<object>.OkWithMessage("Created success"));
            }
            catch (ErrorResponse e)
            {
                return Ok(MyResponse<object>.FailWithMessage(e.Error.Message));
            }
        }

        [HttpDelete]
        [CasbinAuthorize]
        public async Task<IActionResult> RemovePolicy([FromBody] RemovePolicyRequest removePolicyRequest)
        {
            try
            {
                await _casbinService.RemovePolicy(removePolicyRequest);
                return Ok(MyResponse<object>.OkWithMessage("Deleted success"));
            }
            catch (ErrorResponse e)
            {
                return Ok(MyResponse<object>.FailWithMessage(e.Error.Message));
            }
        }
    }
}