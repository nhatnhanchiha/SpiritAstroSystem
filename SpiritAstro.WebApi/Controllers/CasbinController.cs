using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpiritAstro.BusinessTier.Generations.Services;
using SpiritAstro.BusinessTier.Requests.Casbin;
using SpiritAstro.BusinessTier.Responses;
using SpiritAstro.BusinessTier.Services;
using SpiritAstro.DataTier.Models;
using SpiritAstro.WebApi.Attributes;

namespace SpiritAstro.WebApi.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class CasbinController : ControllerBase
    {
        private readonly ICasbinService _casbinService;
        private readonly ICasbinRuleService _casbinRuleService;

        public CasbinController(ICasbinService casbinService, ICasbinRuleService casbinRuleService)
        {
            _casbinService = casbinService;
            _casbinRuleService = casbinRuleService;
        }


        [HttpGet]
        [CasbinAuthorize]
        public async Task<IActionResult> GetAllPolicy([FromQuery] int page, [FromQuery] int limit)
        {
            var casbinRules = await _casbinRuleService.GetCasbinRules(page, limit);
            return Ok(MyResponse<PageResult<CasbinRule>>.OkWithData(casbinRules));
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