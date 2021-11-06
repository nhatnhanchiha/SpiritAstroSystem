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

namespace SpiritAstro.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesControllersController : ControllerBase
    {
        [HttpPost("uploadAvatar")]
        public async Task<IActionResult> UploadAvatar(IFormFile file)
        {
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