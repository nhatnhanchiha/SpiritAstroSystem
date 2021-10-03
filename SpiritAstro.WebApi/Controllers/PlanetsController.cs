using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpiritAstro.BusinessTier.Generations.Services;
using SpiritAstro.BusinessTier.Requests.Planet;
using SpiritAstro.BusinessTier.Responses;
using SpiritAstro.BusinessTier.ViewModels.Planet;

namespace SpiritAstro.WebApi.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class PlanetsController : ControllerBase
    {
        private readonly IPlanetService _planetService;

        public PlanetsController(IPlanetService planetService)
        {
            _planetService = planetService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPlanets([FromQuery] PlanetModel filter, [FromQuery] string[] fields,
            string sort, int page, int limit)
        {
            try
            {
                var allPlanets = await _planetService.GetAllPlanets(filter, fields, sort, page, limit);
                return Ok(MyResponse<PageResult<PlanetModel>>.OkWithData(allPlanets));
            }
            catch (ErrorResponse e)
            {
                return Ok(MyResponse<object>.FailWithMessage(e.Error.Message));
            }
        }

        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetPlanetById(long id)
        {
            try
            {
                var planetModel = await _planetService.GetPlanetById(id);
                return Ok(MyResponse<PlanetModel>.OkWithData(planetModel));
            }
            catch (ErrorResponse e)
            {
                return e.Error.Code switch
                {
                    (int)HttpStatusCode.NotFound => Ok(MyResponse<object>.FailWithMessage(e.Error.Message)),
                    _ => Ok(MyResponse<object>.FailWithMessage(e.Error.Message))
                };
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreatePlanet([FromBody] CreatePlanetRequest createPlanetRequest)
        {
            try
            {
                var planetId = await _planetService.CreatePlanet(createPlanetRequest);
                return Ok(MyResponse<long>.OkWithDetail(planetId, $"Created success planet with id = {planetId}"));
            }
            catch (ErrorResponse e)
            {
                return e.Error.Code switch
                {
                    _ => Ok(MyResponse<object>.FailWithMessage(e.Error.Message))
                };
            }
        }

        [HttpPut("{id:long}")]
        public async Task<IActionResult> UpdatePlanet(long id, [FromBody] UpdatePlanetRequest updatePlanetRequest)
        {
            try
            {
                await _planetService.UpdatePlanet(id, updatePlanetRequest);
                return Ok(MyResponse<object>.OkWithMessage("Updated success"));
            }
            catch (ErrorResponse e)
            {
                return e.Error.Code switch
                {
                    (int)HttpStatusCode.NotFound => Ok(MyResponse<object>.FailWithMessage(e.Error.Message)),
                    _ => Ok(MyResponse<object>.FailWithMessage(e.Error.Message))
                };
            }
        }

        [HttpDelete("{id:long}")]
        public async Task<IActionResult> DeletePlanet(long id)
        {
            try
            {
                await _planetService.DeletePlanetById(id);
                return Ok(MyResponse<object>.OkWithMessage("Deleted success"));
            }
            catch (ErrorResponse e)
            {
                return e.Error.Code switch
                {
                    (int)HttpStatusCode.NotFound => Ok(MyResponse<object>.FailWithMessage(e.Error.Message)),
                    _ => Ok(MyResponse<object>.FailWithMessage(e.Error.Message))
                };
            }
        }
    }
}