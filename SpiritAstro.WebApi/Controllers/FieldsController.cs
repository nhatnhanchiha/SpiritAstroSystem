using Microsoft.AspNetCore.Mvc;
using SpiritAstro.BusinessTier.Entities;
using SpiritAstro.BusinessTier.Generations.Services;
using SpiritAstro.BusinessTier.Requests.Field;
using SpiritAstro.BusinessTier.Responses;
using SpiritAstro.BusinessTier.ViewModels.Field;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using SpiritAstro.WebApi.Attributes;

namespace SpiritAstro.WebApi.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class FieldsController : ControllerBase
    {
        private readonly IFieldService _fieldService;

        public FieldsController(IFieldService fieldService)
        {
            _fieldService = fieldService;
        }

        [HttpGet]
        public async Task<IActionResult> GetListField([FromQuery] FieldModel fieldFilter, int page, int limit, [FromQuery] string[] fields, string sort)
        {
            try
            {
                var fieldModels = await _fieldService.GetListField(fieldFilter, page, limit, fields, sort);
                return Ok(MyResponse<PageResult<FieldModel>>.OkWithData(fieldModels));
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
        [CasbinAuthorize]
        public async Task<IActionResult> CreateNewField([FromBody] CreateFieldRequest createFieldRequest)
        {

            try
            {
                
                var fieldId = await _fieldService.CreateField(createFieldRequest);
                return Ok(MyResponse<long>.OkWithDetail(fieldId,
                    $"Created success field with id = {fieldId}"));
            }
            catch (ErrorResponse e)
            {
                return Ok(MyResponse<object>.FailWithMessage(e.Error.Message));
            }
        }

        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetFieldById(long id)
        {
            try
            {
                var fieldModel = await _fieldService.GetFieldById(id);
                return Ok(MyResponse<FieldModel>.OkWithData(fieldModel));
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

        [HttpPut("{id:long}")]
        [CasbinAuthorize]
        public async Task<IActionResult> UpdateField(long id, [FromBody] UpdateFieldRequest updateFieldRequest)
        {
            //var claims = (CustomClaims)HttpContext.Items["claims"];

            try
            {
                //if (claims == null || claims.Field)
                //{
                //    return Ok(MyResponse<object>.FailWithMessage("Access denied !"));
                //}

                await _fieldService.UpdateField(id, updateFieldRequest);
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
        public async Task<IActionResult> DeleteField(long id)
        {
            try
            {
                await _fieldService.DeleteField(id);
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
