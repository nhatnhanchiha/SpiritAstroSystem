﻿using Microsoft.AspNetCore.Mvc;
using SpiritAstro.BusinessTier.Entities;
using SpiritAstro.BusinessTier.Generations.Services;
using SpiritAstro.BusinessTier.Requests.Field;
using SpiritAstro.BusinessTier.Responses;
using SpiritAstro.BusinessTier.ViewModels.Field;
using SpiritAstro.WebApi.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace SpiritAstro.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FieldController : ControllerBase
    {
        private readonly IFieldService _fieldService;

        public FieldController(IFieldService fieldService)
        {
            _fieldService = fieldService;
        }

        [HttpPost]
        [CasbinAuthorize]
        public async Task<IActionResult> CreateNewField([FromBody] CreateFieldRequest createFieldRequest)
        {
            var claims = (CustomClaims)HttpContext.Items["claims"];

            try
            {
                if (claims == null)
                {
                    return Ok(MyResponse<object>.FailWithMessage("Access denied !"));
                }

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
                var categoryModel = await _fieldService.GetFieldById(id);
                return Ok(MyResponse<FieldModel>.OkWithData(categoryModel));
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
