using Microsoft.AspNetCore.Mvc;
using SpiritAstro.BusinessTier.Generations.Services;
using SpiritAstro.BusinessTier.Requests.Field;
using SpiritAstro.BusinessTier.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public async Task<IActionResult> CreateNewField([FromBody] CreateFieldRequest createFieldRequest)
        {
            try
            {
                var fieldID = await _fieldService.CreateField(createFieldRequest);
                return Ok(MyResponse<long>.OkWithDetail(fieldID,
                    $"Created success category with id = {fieldID}"));
            }
            catch (ErrorResponse e)
            {
                return Ok(MyResponse<object>.FailWithMessage(e.Error.Message));
            }
        }
    }
}
