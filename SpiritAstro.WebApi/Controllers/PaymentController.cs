using Microsoft.AspNetCore.Mvc;
using SpiritAstro.BusinessTier.Generations.Services;
using SpiritAstro.BusinessTier.Requests.Payment;
using SpiritAstro.BusinessTier.Responses;
using SpiritAstro.BusinessTier.ViewModels.Payment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace SpiritAstro.WebApi.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateNewPayment([FromBody] CreatePaymentRequest createPayment)
        {
            try
            {
                var paymentId = await _paymentService.CreatePayment(createPayment);
                return Ok(MyResponse<long>.OkWithDetail(paymentId,
                    $"Created success payment with id = {paymentId}"));
            }
            catch (ErrorResponse e)
            {
                return Ok(MyResponse<object>.FailWithMessage(e.Error.Message));
            }
        }

        [HttpPut("{id:long}")]
        public async Task<IActionResult> UpdatePayment(long id, [FromBody] UpdatePaymentRequest updatePayment)
        {

            try
            {
                await _paymentService.UpdatePayment(id, updatePayment);
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
        public async Task<IActionResult> GetPaymentById(long id)
        {
            try
            {
                var paymentModel = await _paymentService.GetPaymentById(id);
                return Ok(MyResponse<PaymentModel>.OkWithData(paymentModel));
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
        public async Task<IActionResult> GetListPayment([FromQuery] PaymentModel paymentFilter, int page, int limit, [FromQuery] string[] fields, string sort)
        {
            try
            {
                var paymentModels = await _paymentService.GetListPayment(paymentFilter, page, limit, fields, sort);
                return Ok(MyResponse<PageResult<PaymentModel>>.OkWithData(paymentModels));
            }
            catch (ErrorResponse e)
            {
                return e.Error.Code switch
                {
                    _ => Ok(MyResponse<object>.FailWithMessage(e.Error.Message))
                };
            }
        }

        [HttpDelete("{id:long}")]
        public async Task<IActionResult> DeletePayment(long id)
        {
            try
            {
                await _paymentService.DeletePayment(id);
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
