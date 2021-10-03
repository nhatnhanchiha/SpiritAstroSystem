using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpiritAstro.BusinessTier.Entities;
using SpiritAstro.BusinessTier.Generations.Services;
using SpiritAstro.BusinessTier.Requests.Customer;
using SpiritAstro.BusinessTier.Responses;
using SpiritAstro.BusinessTier.ViewModels.Astrologer;
using SpiritAstro.BusinessTier.ViewModels.Customer;

namespace SpiritAstro.WebApi.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        // private readonly IUserRoleService

        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCustomers([FromQuery] PublicCustomerModel filter, [FromQuery] string[] fields, string sort, int page, int limit)
        {
            try
            {
                var customers = await _customerService.GetAllCustomers(filter, fields, sort, page, limit);
                return Ok(MyResponse<PageResult<PublicCustomerModel>>.OkWithData(customers));
            }
            catch (ErrorResponse e)
            {
                return Ok(MyResponse<object>.FailWithMessage(e.Error.Message));
            }
        }

        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetCustomerById(long id)
        {
            var claims = (CustomClaims)HttpContext.Items["claims"];

            try
            {
                if (claims == null || claims.UserId != id)
                {
                    var publicCustomerModel = await _customerService.GetPublicCustomerById(id);
                    return Ok(MyResponse<PublicCustomerModel>.OkWithData(publicCustomerModel));
                }

                var customerModel = await _customerService.GetCustomerById(id);
                return Ok(MyResponse<CustomerModel>.OkWithData(customerModel));
            }
            catch (ErrorResponse e)
            {
                return e.Error.Code switch
                {
                    (int)HttpStatusCode.NotFound => Ok(MyResponse<object>.FailWithMessage(e.Error.Message)),
                    _ => Ok(MyResponse<object>.FailWithMessage(e.Error.Message))
                };
            }
        }

        [HttpPost]
        public async Task<IActionResult> RegisterACustomer([FromBody] RegisterCustomerRequest registerCustomerRequest)
        {
            var claims = (CustomClaims)HttpContext.Items["claims"];

            var userId = claims!.UserId;

            try
            {
                await _customerService.RegisterACustomer(userId, registerCustomerRequest);
                // Todo: Create user role
                return Ok(MyResponse<object>.OkWithMessage("Registered success"));
            }
            catch (ErrorResponse e)
            {
                return e.Error.Code switch
                {
                    (int)HttpStatusCode.NotFound => Ok(MyResponse<object>.FailWithMessage(e.Error.Message)),
                    (int)HttpStatusCode.BadRequest => Ok(MyResponse<object>.FailWithMessage(e.Error.Message)),
                    _ => Ok(MyResponse<object>.FailWithMessage(e.Error.Message))
                };
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateACustomer([FromBody] UpdateCustomerRequest updateCustomerRequest)
        {
            var claims = (CustomClaims)HttpContext.Items["claims"];

            var id = claims!.UserId;

            try
            {
                await _customerService.UpdateCustomer(id, updateCustomerRequest);
                return Ok(MyResponse<object>.OkWithMessage("Updated success"));
            }
            catch (ErrorResponse e)
            {
                return e.Error.Code switch
                {
                    (int)HttpStatusCode.NotFound => Ok(MyResponse<object>.FailWithMessage(e.Error.Message)),
                    _ => Ok(MyResponse<object>.FailWithMessage(e.Error.Message))
                };
            }
        }

        [HttpDelete("{id:long}")]
        public async Task<IActionResult> DeleteCustomerById(long id)
        {
            try
            {
                await _customerService.DeleteCustomer(id);
                // Todo: Delete user role
                return Ok(MyResponse<object>.OkWithMessage("Deleted success"));
            }
            catch (ErrorResponse e)
            {
                return e.Error.Code switch
                {
                    (int)HttpStatusCode.NotFound => Ok(MyResponse<object>.FailWithMessage(e.Error.Message)),
                    _ => Ok(MyResponse<object>.FailWithMessage(e.Error.Message))
                };
            }
        }
    }
}