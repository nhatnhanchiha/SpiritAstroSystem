using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgoraIO.Media;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpiritAstro.BusinessTier.Commons.Constants;
using SpiritAstro.BusinessTier.Entities;
using SpiritAstro.BusinessTier.Requests.Agora;
using SpiritAstro.BusinessTier.Responses;
using SpiritAstro.BusinessTier.Responses.Agora;
using SpiritAstro.BusinessTier.Services;
using SpiritAstro.WebApi.Attributes;

namespace SpiritAstro.WebApi.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class AgoraController : ControllerBase
    {
        private readonly IRedisService _redisService;

        public AgoraController(IRedisService redisService)
        {
            _redisService = redisService;
        }

        [HttpGet("start-a-channel")]
        [CasbinAuthorize]
        public IActionResult StartAChanel()
        {
            var claims = (CustomClaims)HttpContext.Items["claims"];

            var channelName = "astro_" + claims!.UserId;
            
            var unixStart = DateTime.SpecifyKind(new DateTime(1970, 1, 1), DateTimeKind.Utc);
            var epoch = (uint)Math.Floor((DateTime.Now.Add(TimeSpan.FromHours(24)).ToUniversalTime() - unixStart).TotalSeconds);
            var token = new AccessToken(AgoraConstant.AppId, AgoraConstant.AppCertificate, channelName, "0");
            
            token.addPrivilege(Privileges.kRtmLogin, epoch);
            token.addPrivilege(Privileges.kJoinChannel, epoch);
            token.addPrivilege(Privileges.kPublishVideoStream, epoch);
            token.addPrivilege(Privileges.kPublishAudioStream, epoch);

            var response = new AgoraResponse
            {
                Chanel = channelName,
                Token = token.build()
            };

            _redisService.CacheToRedis(channelName, response, TimeSpan.FromHours(24));
            return Ok(MyResponse<AgoraResponse>.OkWithData(response));
        }
        
        [HttpGet("stop-a-channel")]
        [CasbinAuthorize]
        public async Task<IActionResult> StopAChanel()
        {
            var claims = (CustomClaims)HttpContext.Items["claims"];

            var channelName = "astro_" + claims!.UserId;

            await _redisService.DeleteFromRedis(channelName);

            return Ok(MyResponse<object>.OkWithMessage("Stopped success"));
        }

        [HttpGet("get-info-for-customer")]
        [CasbinAuthorize]
        public async Task<IActionResult> GetAgoraInfo([FromQuery] long astrologerId)
        {
            var channelName = "astro_" + astrologerId;
            var agoraResponse = await _redisService.GetFromRedis<AgoraResponse>(channelName);
            if (agoraResponse == null)
            {
                return Ok(MyResponse<object>.FailWithMessage("This astrologer is not ready for meeting!"));
            }

            return Ok(MyResponse<AgoraResponse>.OkWithData(agoraResponse));
        }


    }
}