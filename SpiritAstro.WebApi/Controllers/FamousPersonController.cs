using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpiritAstro.BusinessTier.Generations.Services;
using SpiritAstro.BusinessTier.Requests.FamousPerson;
using SpiritAstro.BusinessTier.Responses;
using SpiritAstro.BusinessTier.ViewModels.FamousPerson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace SpiritAstro.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FamousPersonController : ControllerBase
    {
        private readonly IFamousPersonService _famouspersonService;

        public FamousPersonController(IFamousPersonService famouspersonService)
        {
            _famouspersonService = famouspersonService;
        }

        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetFamousPersonById(long id)
    {
            try
            {
                var famoupersonModel = await _famouspersonService.GetFamousPersonById(id);
                return Ok(MyResponse<FamousPersonModel>.OkWithData(famoupersonModel));
            }
            catch (ErrorResponse e)
            {
                if(e.Error.Code == (int)HttpStatusCode.NotFound)
                {
                    return Ok(MyResponse<object>.FailWithMessage(e.Error.Message));
                }

                return Ok(MyResponse<object>.FailWithMessage(e.Error.Message));
            }
    }
        [HttpPost]
        public async Task<IActionResult> CreateNewFamousPerson([FromBody] CreateFamousPersonRequest createFamousPersonRequest)
        {
            try
            {
                var famouspersonId = await _famouspersonService.CreateFamousPerson(createFamousPersonRequest);
                return Ok(MyResponse<long>.OkWithDetail(famouspersonId, "Created success famous person with id = {famouspersonId}"));
            }
            catch (ErrorResponse e)
            {
                return Ok(MyResponse<object>.FailWithMessage(e.Error.Message));
            }
            }
        [HttpPut("{id:long}")]
        public async Task<IActionResult> UpdateFamousPerson(long id, [FromBody] UpdateFamousPersonRequest updateCategoryRequest)
        {
            try
            {
                await _famouspersonService.UpdateFamousPerson(id, updateCategoryRequest);
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
        public async Task<IActionResult> DeleteFamousPerson(long id)
        {
            try
            {
                await _famouspersonService.DeleteFamousPerson(id);
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
