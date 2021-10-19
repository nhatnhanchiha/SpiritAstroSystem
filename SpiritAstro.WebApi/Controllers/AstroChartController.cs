using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpiritAstro.BusinessTier.Commons.Toolkit.Zodiac;
using SpiritAstro.BusinessTier.Requests.AstroChart;
using SpiritAstro.BusinessTier.Services;

namespace SpiritAstro.WebApi.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class AstroChartController : ControllerBase
    {
        private readonly IAstroChartService _astroChartService;

        public AstroChartController(IAstroChartService astroChartService)
        {
            _astroChartService = astroChartService;
        }

        [HttpGet]
        public IActionResult GetChart([FromQuery] GetNatalChartRequest request)
        {
            var natalChartDataResponse = _astroChartService.Execute(request);
            
            var zodiacPositionHouse = new ZodiacPositionHouse();
            zodiacPositionHouse.Initialize(natalChartDataResponse);
            
            var bitmap = Bitmap.FromFile(@"Resources/background.jpg");
            var g = Graphics.FromImage(bitmap);
            zodiacPositionHouse.Draw(g);
            g.Flush();
            
            var memoryStream = new MemoryStream();
            bitmap.Save(memoryStream, ImageFormat.Png);
            
            var data = memoryStream.ToArray();
            return File(data, "image/png");
        }
    }
}