using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SpiritAstro.BusinessTier.Generations.Services;
using SpiritAstro.BusinessTier.Requests.FieldDetail;
using SpiritAstro.BusinessTier.Responses;
using SpiritAstro.BusinessTier.ViewModels.FieldDetail;

namespace SpiritAstro.WebApi.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class FieldDetailsController : ControllerBase
    {
        private readonly IFieldDetailService _fieldDetailService;

        public FieldDetailsController(IFieldDetailService fieldDetailService)
        {
            _fieldDetailService = fieldDetailService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllFieldDetails([FromQuery] FieldDetailModel filter, [FromQuery] string[] fields, string sort, int page, int limit)
        {
            try
            {
                var allFieldDetails = await _fieldDetailService.GetAllFieldDetail(filter, fields, sort, page, limit);
                return Ok(MyResponse<PageResult<FieldDetailModel>>.OkWithData(allFieldDetails));
            }
            catch (ErrorResponse e)
            {
                return Ok(MyResponse<object>.FailWithMessage(e.Error.Message));
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateFieldDetail([FromBody] CreateFieldDetailRequest createFieldDetailRequest)
        {
            try
            {
                await _fieldDetailService.CreateFieldDetail(createFieldDetailRequest);
                return Ok(MyResponse<object>.OkWithMessage("Created success"));
            }
            catch (ErrorResponse e)
            {
                return e.Error.Code switch
                {
                    (int)HttpStatusCode.BadRequest => Ok(MyResponse<object>.FailWithMessage(e.Error.Message)),
                    _ => Ok(MyResponse<object>.FailWithMessage(e.Error.Message))
                };
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateFieldDetail([FromBody] UpdateFieldDetailRequest updateFieldDetailRequest)
        {
            try
            {
                await _fieldDetailService.UpdateFieldDetail(updateFieldDetailRequest);
                return Ok(MyResponse<object>.OkWithMessage("Updated success"));
            }
            catch (ErrorResponse e)
            {
                return e.Error.Code switch
                {
                    (int)HttpStatusCode.BadRequest => Ok(MyResponse<object>.FailWithMessage(e.Error.Message)),
                    _ => Ok(MyResponse<object>.FailWithMessage(e.Error.Message))
                };
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteFieldDetail([FromBody] DeleteFieldDetailRequest deleteFieldDetailRequest)
        {
            try
            {
                await _fieldDetailService.DeleteFieldDetail(deleteFieldDetailRequest);
                return Ok(MyResponse<object>.OkWithMessage("Deleted success"));
            }
            catch (ErrorResponse e)
            {
                return e.Error.Code switch
                {
                    (int)HttpStatusCode.BadRequest => Ok(MyResponse<object>.FailWithMessage(e.Error.Message)),
                    _ => Ok(MyResponse<object>.FailWithMessage(e.Error.Message))
                };
            }
        }
    }
}