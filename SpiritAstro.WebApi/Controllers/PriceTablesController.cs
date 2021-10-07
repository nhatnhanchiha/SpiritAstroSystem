using Microsoft.AspNetCore.Mvc;
using SpiritAstro.BusinessTier.Generations.Services;
using SpiritAstro.BusinessTier.Requests.PriceTable;
using SpiritAstro.BusinessTier.Responses;
using SpiritAstro.BusinessTier.ViewModels.PriceTable;
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
    public class PriceTablesController : ControllerBase
    {
        private readonly IPriceTableService _priceTableService;

        public PriceTablesController(IPriceTableService priceTable)
        {
            _priceTableService = priceTable;
        }

        [HttpPost]
        [CasbinAuthorize]
        public async Task<IActionResult> CreatePriceTable([FromBody] CreatePriceTableRequest createPriceTable)
        {

            try
            {

                var priceTableId = await _priceTableService.CreatePriceTable(createPriceTable);
                return Ok(MyResponse<long>.OkWithDetail(priceTableId,
                $"Created success price_table with id = {priceTableId}"));
            }
            catch (ErrorResponse e)
            {
                return Ok(MyResponse<object>.FailWithMessage(e.Error.Message));
            }
        }

        [HttpPut("{id:long}")]
        [CasbinAuthorize]
        public async Task<IActionResult> UpdatePriceTable(long id, [FromBody] UpdatePriceTableRequest updatePriceTable)
        {
            try
            {

                await _priceTableService.UpdatePriceTable(id, updatePriceTable);
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

        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetPriceTableById(long id)
        {
            try
            {
                var priceTableModel = await _priceTableService.GetPriceTableById(id);
                return Ok(MyResponse<PriceTableModel>.OkWithData(priceTableModel));
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

        [HttpGet]
        public async Task<IActionResult> GetListPriceTable([FromQuery] PriceTableModel priceTableFilter, int page, int limit, [FromQuery] string[] fields, string sort)
        {
            try
            {
                var priceTableModels = await _priceTableService.GetListPriceTable(priceTableFilter, page, limit, fields, sort);
                return Ok(MyResponse<PageResult<PriceTableModel>>.OkWithData(priceTableModels));
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
