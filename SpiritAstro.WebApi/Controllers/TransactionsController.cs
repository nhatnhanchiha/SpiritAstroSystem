using Microsoft.AspNetCore.Mvc;
using SpiritAstro.BusinessTier.Generations.Services;
using SpiritAstro.BusinessTier.Requests.Transaction;
using SpiritAstro.BusinessTier.Responses;
using SpiritAstro.BusinessTier.ViewModels.Transaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SpiritAstro.WebApi.Attributes;

namespace SpiritAstro.WebApi.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionService _transactionService;

        public TransactionsController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpPost]
        [CasbinAuthorize]
        public async Task<IActionResult> CreateTransaction([FromBody] CreateTransactionRequest createTransaction)
        {
            try
            {
                Console.WriteLine("abc");
                var paymentId = await _transactionService.CreateTransaction(createTransaction);
                return Ok(MyResponse<long>.OkWithDetail(paymentId,
                    $"Created success transaction with PaymentId = {paymentId}"));
            }
            catch (ErrorResponse e)
            {
                return Ok(MyResponse<object>.FailWithMessage(e.Error.Message));
            }
        }
        [HttpGet]
        [CasbinAuthorize]
        public async Task<IActionResult> GetListTransaction([FromQuery] TransactionModel transactionFilter, [FromQuery] string[] fields, string sort, int page, int limit)
        {
            try
            {
                var pzModels = await _transactionService.GetListTransaction(transactionFilter, page, limit, sort, fields);
                return Ok(MyResponse<PageResult<TransactionModel>>.OkWithData(pzModels));
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
