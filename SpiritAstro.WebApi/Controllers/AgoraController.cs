using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgoraIO.Media;
using Google.Cloud.Firestore;
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
        private readonly IAstroOnlineService _astroOnlineService;

        public AgoraController(IRedisService redisService, IAstroOnlineService astroOnlineService)
        {
            _redisService = redisService;
            _astroOnlineService = astroOnlineService;
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

            var response = new AgoraObject
            {
                Chanel = channelName,
                Token = token.build(),
                IsLock = false,
                EndTime = DateTime.Now.Add(TimeSpan.FromHours(24))
            };

            _redisService.CacheToRedis(channelName, response, TimeSpan.FromHours(24));

            if (_astroOnlineService.IsLock())
            {
                while (_astroOnlineService.IsLock())
                {
                    
                }
            }
            
            if (!_astroOnlineService.IsLock())
            {
                _astroOnlineService.SwitchLock();
                _astroOnlineService.AddAstroId(claims.UserId);
                _astroOnlineService.SwitchLock();
            }
            
            return Ok(MyResponse<AgoraObject>.OkWithData(response));
        }

        [HttpGet("test")]
        public IActionResult GetListOnline()
        {
            return Ok(_astroOnlineService.GetSetAstroOnline().ToList());
        }
        
        [HttpGet("stop-a-channel")]
        [CasbinAuthorize]
        public async Task<IActionResult> StopAChanel()
        {
            var claims = (CustomClaims)HttpContext.Items["claims"];

            var channelName = "astro_" + claims!.UserId;

            await _redisService.DeleteFromRedis(channelName);
            
            if (_astroOnlineService.IsLock())
            {
                while (_astroOnlineService.IsLock())
                {
                    
                }
            }
            
            if (!_astroOnlineService.IsLock())
            {
                _astroOnlineService.SwitchLock();
                _astroOnlineService.RemoveAstroId(claims.UserId);
                _astroOnlineService.SwitchLock();
            }

            return Ok(MyResponse<object>.OkWithMessage("Stopped success"));
        }

        [HttpGet("get-info-for-customer")]
        [CasbinAuthorize]
        public async Task<IActionResult> GetAgoraInfo([FromQuery] long astrologerId)
        {
            var channelName = "astro_" + astrologerId;
            var agoraObject = await _redisService.GetFromRedis<AgoraObject>(channelName);
            
            if (agoraObject == null)
            {
                return Ok(MyResponse<object>.FailWithMessage("Nhà tư vấn hiện không online."));
            }

            if (agoraObject.IsLock)
            {
                return Ok(MyResponse<object>.FailWithMessage("Nhà tư vấn này đang bận. Xin thử lại sau."));
            }
            
            agoraObject.SwitchLock();

            var test = agoraObject.EndTime.Subtract(DateTime.Now).TotalHours;
            
            
            await _redisService.CacheToRedis(channelName, agoraObject, TimeSpan.FromHours(agoraObject.EndTime.Subtract(DateTime.Now).TotalHours));
            
            return Ok(MyResponse<AgoraObject>.OkWithData(agoraObject));
        }

        [HttpGet("customer-end-call")]
        [CasbinAuthorize]
        public async Task<IActionResult> CustomerEndCall([FromQuery] long astrologerId)
        {
            var channelName = "astro_" + astrologerId;
            var agoraObject = await _redisService.GetFromRedis<AgoraObject>(channelName);
            
            if (agoraObject == null)
            {
                return Ok(MyResponse<object>.FailWithMessage("Error"));
            }

            if (!agoraObject.IsLock)
            {
                return Ok(MyResponse<object>.FailWithMessage("Error"));
            }
            
            agoraObject.SwitchLock();
            
            await _redisService.CacheToRedis(channelName, agoraObject, TimeSpan.FromHours(agoraObject.EndTime.Subtract(DateTime.Now).TotalHours));
            
            return Ok(MyResponse<AgoraObject>.OkWithData(agoraObject));
        }
    }
}