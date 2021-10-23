using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpiritAstro.BusinessTier.Entities;
using SpiritAstro.BusinessTier.Generations.Services;
using SpiritAstro.BusinessTier.Requests.Token;
using SpiritAstro.BusinessTier.Responses;
using SpiritAstro.WebApi.Attributes;

namespace SpiritAstro.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokensController : ControllerBase
    {
        private readonly ITokenService _tokenService;

        public TokensController(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        [HttpPost]
        [CustomAuthorize]
        public async Task<IActionResult> RegisterToken([FromBody] CreateTokenRequest createTokenRequest)
        {
            var claims = (CustomClaims)HttpContext.Items["claims"];

            var id = claims!.UserId;

            await _tokenService.RegisterAToken(id, createTokenRequest);

            return Ok(MyResponse<object>.OkWithMessage("Registered success"));
        }
    }
}