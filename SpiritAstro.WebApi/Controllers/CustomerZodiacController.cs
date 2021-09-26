using Microsoft.AspNetCore.Mvc;
using SpiritAstro.BusinessTier.Generations.Services;
using SpiritAstro.BusinessTier.Requests.CustomerZodiac;
using SpiritAstro.BusinessTier.Responses;
using SpiritAstro.BusinessTier.ViewModels.CustomerZodiac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace SpiritAstro.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerZodiacController : ControllerBase
    {
        private readonly ICustomerZodiacService _customerZodiacService;

        public CustomerZodiacController(ICustomerZodiacService customerZodiacService)
        {
            _customerZodiacService = customerZodiacService;
        }

        [HttpGet]
        public async Task<IActionResult> GetListCustomerZodiac([FromQuery] CustomerZodiacModel customerZodiacFilter, int page, int limit)
        {
            try
            {
                var customerZodiacModels = await _customerZodiacService.GetListCustomerZodiac(customerZodiacFilter, page, limit);
                return Ok(MyResponse<PageResult<CustomerZodiacModel>>.OkWithData(customerZodiacModels));
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
        public async Task<IActionResult> CreateNewCustomerZodiac([FromBody] CustomerZodiacRequest createRequest)
        {
            try
            {
                var czID = await _customerZodiacService.CreateCustomerZodiac(createRequest);
                return Ok(MyResponse<long>.OkWithDetail(czID,
                    $"Created success with id = {czID}"));
            }
            catch (ErrorResponse e)
            {
                return Ok(MyResponse<object>.FailWithMessage(e.Error.Message));
            }
        }

        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetCustomerZodiacById(long id)
        {
            try
            {
                var czModel = await _customerZodiacService.GetCustomerZodiacById(id);
                return Ok(MyResponse<CustomerZodiacModel>.OkWithData(czModel));
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
    }
}
