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
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class FamousPersonsController : ControllerBase
    {
        private readonly IFamousPersonService _famousPersonService;

        public FamousPersonsController(IFamousPersonService famousPersonService)
        {
            _famousPersonService = famousPersonService;
        }

        [HttpGet]
        public async Task<IActionResult> GetListFamousPersons([FromQuery]FamousPersonModel famousPersonFilter, int page, int limit)
        {
            try
            {
                var famousPersonModels = await _famousPersonService.GetListFamousPerson(famousPersonFilter, page, limit);
                return Ok(MyResponse<PageResult<FamousPersonModel>>.OkWithData(famousPersonModels));
            }
            catch (ErrorResponse e)
            {
                return e.Error.Code switch
                {
                    _ => Ok(MyResponse<object>.FailWithMessage(e.Error.Message))
                };
            }
        }

        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetFamousPersonById(long id)
        {
            try
            {
                var famousPersonModel = await _famousPersonService.GetFamousPersonById(id);
                return Ok(MyResponse<FamousPersonModel>.OkWithData(famousPersonModel));
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
        public async Task<IActionResult> CreateNewFamousPerson(
            [FromBody] CreateFamousPersonRequest createFamousPersonRequest)
        {
            try
            {
                var famousPersonId = await _famousPersonService.CreateFamousPerson(createFamousPersonRequest);
                return Ok(MyResponse<long>.OkWithDetail(famousPersonId,
                    $"Created success famous person with id = {famousPersonId}"));
            }
            catch (ErrorResponse e)
            {
                return Ok(MyResponse<object>.FailWithMessage(e.Error.Message));
            }
        }

        [HttpPut("{id:long}")]
        public async Task<IActionResult> UpdateFamousPerson(long id,
            [FromBody] UpdateFamousPersonRequest updateCategoryRequest)
        {
            try
            {
                await _famousPersonService.UpdateFamousPerson(id, updateCategoryRequest);
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
                await _famousPersonService.DeleteFamousPerson(id);
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