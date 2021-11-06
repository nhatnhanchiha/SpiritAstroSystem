using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Firebase.Auth;
using Firebase.Storage;
using FirebaseAdmin;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SpiritAstro.BusinessTier.Commons.Constants;
using SpiritAstro.BusinessTier.Responses;
using SpiritAstro.BusinessTier.Services;
using SpiritAstro.WebApi.Attributes;

namespace SpiritAstro.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        [HttpPost("uploadAvatar")]
        [CustomAuthorize]
        public async Task<IActionResult> UploadAvatar(IFormFile file)
        {
            if (file.Length > 25000000)
                return BadRequest(new ErrorResponse(400, "Exceed 25MB"));
            
            using (var client = new HttpClient())
            {
                using (var content =
                    new MultipartFormDataContent())
                {
                    content.Add(new StreamContent(file.OpenReadStream()), "upload[]", "upload.jpg");

                    using (
                        var message =
                            await client.PostAsync("http://localhost:8080/uploadAvatar", content))
                    {
                        var input = await message.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<List<string>>(input);
                        if (result == null)
                        {
                            return Ok(MyResponse<object>.FailWithMessage("Upload fail"));
                        }

                        return Ok(MyResponse<string>.OkWithData(result.First()));
                    }
                }
            }
        }
    }
}