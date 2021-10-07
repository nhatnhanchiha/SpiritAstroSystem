using Microsoft.AspNetCore.Mvc;
using SpiritAstro.BusinessTier.Generations.Services;
using SpiritAstro.BusinessTier.Requests.Zodiac;
using SpiritAstro.BusinessTier.Responses;
using SpiritAstro.BusinessTier.ViewModels.Zodiac;
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
    public class ZodiacsController : ControllerBase
    {
        private readonly IZodiacService _zodiacService;

        public ZodiacsController(IZodiacService famousPersonService)
        {
            _zodiacService = famousPersonService;
        }

        [HttpGet]
        public async Task<IActionResult> GetListZodiac([FromQuery] ZodiacModel zodiacFilter, [FromQuery] string[] fields, string sort, int page, int limit)
        {
            try
            {
                var zodiacModel = await _zodiacService.GetListZodiac(zodiacFilter, fields, sort, page, limit);
                return Ok(MyResponse<PageResult<ZodiacModel>>.OkWithData(zodiacModel));
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
        public async Task<IActionResult> GetZodiacById(long id)
        {
            try
            {
                var famousPersonModel = await _zodiacService.GetZodiacById(id);
                return Ok(MyResponse<ZodiacModel>.OkWithData(famousPersonModel));
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
        [CasbinAuthorize]
        public async Task<IActionResult> CreateZodiac([FromBody] CreateZodiacRequest createZodiac)
        {
            try
            {
                var zodiacId = await _zodiacService.CreateZodiac(createZodiac);
                return Ok(MyResponse<long>.OkWithDetail(zodiacId,
                    $"Created success zodiac with Id = {zodiacId}"));
            }
            catch (ErrorResponse e)
            {
                return Ok(MyResponse<object>.FailWithMessage(e.Error.Message));
            }
        }

        [HttpPut("{id:long}")]
        [CasbinAuthorize]
        public async Task<IActionResult> UpdateZodiac(long id,
            [FromBody] UpdateZodiacRequest updateZodiacRequest)
        {
            try
            {
                await _zodiacService.UpdateZodiac(id, updateZodiacRequest);
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
    }
}
