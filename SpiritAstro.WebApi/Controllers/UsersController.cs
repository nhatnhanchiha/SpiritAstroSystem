using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FirebaseAdmin.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpiritAstro.BusinessTier.Requests;

namespace SpiritAstro.WebApi.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        [HttpPost("login-for-customer")]
        public async Task<IActionResult> LoginWithToken()
        {
            var decodedToken = await FirebaseAuth.DefaultInstance
                .VerifyIdTokenAsync(
                    "eyJhbGciOiJSUzI1NiIsImtpZCI6IjdiODcxMTIzNzU0MjdkNjU3ZjVlMjVjYTAxZDU2NWU1OTJhMjMxZGIiLCJ0eXAiOiJKV1QifQ.eyJpc3MiOiJodHRwczovL3NlY3VyZXRva2VuLmdvb2dsZS5jb20vc3Bpcml0YXN0cm8tMmJmYmEiLCJhdWQiOiJzcGlyaXRhc3Ryby0yYmZiYSIsImF1dGhfdGltZSI6MTYzMjkzNDkxMiwidXNlcl9pZCI6IjBiWXg1ZUsyOExYbjVRV3dTUko3QUtoTUNERjMiLCJzdWIiOiIwYll4NWVLMjhMWG41UVd3U1JKN0FLaE1DREYzIiwiaWF0IjoxNjMyOTM0OTEyLCJleHAiOjE2MzI5Mzg1MTIsImVtYWlsIjoic2xpbXZ1QGdtYWlsLmNvbSIsImVtYWlsX3ZlcmlmaWVkIjpmYWxzZSwiZmlyZWJhc2UiOnsiaWRlbnRpdGllcyI6eyJlbWFpbCI6WyJzbGltdnVAZ21haWwuY29tIl19LCJzaWduX2luX3Byb3ZpZGVyIjoicGFzc3dvcmQifX0.f_CxsSnGJ9L35HHwNj3XSCQR675x5YUoPuFPnSE0-4eVjxASkt0PIp0HZCyeuWXb457MRLzsr2SWK6dQ865qKe2hCnNWGr0pbsDl1OuugKDdviwJpWykch14q0_u9CuPjuw0uextrsGhdLIgPqAwpdIT9i6MPVbo5W6sCm4k5UaUvMypwaJ_CYoGD4TVwcLTkSAlcReyoEy8I89BoAXiJ4j2eNsAyaWFFWh6WWQQs04eMYylCkJH2qMK2jgGqxjOD6mTxpYa_bj2M_y2caLX6RC2OJBDQVHUWezlWZxDUJ4dBKP_oOM5XymeKD3jisT3VCow8xs58QDZy6S_bskvbQ");
            var uid = decodedToken.Uid;
            return Ok(uid);
        }
    }
}