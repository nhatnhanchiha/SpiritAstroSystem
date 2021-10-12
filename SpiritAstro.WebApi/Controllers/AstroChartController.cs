using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
            return Ok(_astroChartService.Execute(request));
        }
    }
}