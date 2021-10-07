using Microsoft.AspNetCore.Mvc;
using SpiritAstro.BusinessTier.Generations.Services;
using SpiritAstro.BusinessTier.Responses;
using SpiritAstro.BusinessTier.Requests.Wallet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using SpiritAstro.BusinessTier.ViewModels.Wallet;
using SpiritAstro.WebApi.Attributes;

namespace SpiritAstro.WebApi.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class WalletsController : ControllerBase
    {
        private readonly IWalletService _walletService;

        public WalletsController(IWalletService walletService)
        {
            _walletService = walletService;
        }

        [HttpGet("{id:long}")]
        [CasbinAuthorize]
        public async Task<IActionResult> GetWalletById(long id)
        {
            try
            {
                var walletModel = await _walletService.GetWalletById(id);
                return Ok(MyResponse<WalletModel>.OkWithData(walletModel));
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

        //[HttpPost]
        //public async Task<IActionResult> CreateNewWallet(
        //   [FromBody] CreateWalletRequest createWalletRequest)
        //{
        //    try
        //    {
        //        var walletId = await _walletService.CreateWallet(createWalletRequest);
        //        return Ok(MyResponse<long>.OkWithDetail(walletId,
        //            $"Created success wallet with id = {walletId}"));
        //    }
        //    catch (ErrorResponse e)
        //    {
        //        return Ok(MyResponse<object>.FailWithMessage(e.Error.Message));
        //    }
        //}
    }


}
