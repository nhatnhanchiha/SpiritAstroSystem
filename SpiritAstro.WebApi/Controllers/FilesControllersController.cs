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
using SpiritAstro.BusinessTier.Commons.Constants;
using SpiritAstro.BusinessTier.Services;

namespace SpiritAstro.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesControllersController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Test()
        {
            return Ok();
        }
    }
}