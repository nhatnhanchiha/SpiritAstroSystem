using System.Linq;
using System.Linq.Dynamic.Core;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using SpiritAstro.BusinessTier.Commons.Utils;
using SpiritAstro.BusinessTier.Generations.Repositories;
using SpiritAstro.BusinessTier.Requests.Planet;
using SpiritAstro.BusinessTier.Responses;
using SpiritAstro.BusinessTier.ViewModels.Planet;
using SpiritAstro.DataTier.BaseConnect;
using SpiritAstro.DataTier.Models;

namespace SpiritAstro.BusinessTier.Generations.Services
{
    public partial interface IPlanetService
    {
        Task<PageResult<PlanetModel>> GetAllPlanets(PlanetModel filter, string[] fields,
            string sort, int page, int limit);

        Task<PlanetModel> GetPlanetById(long planetId);
        Task<long> CreatePlanet(CreatePlanetRequest createPlanetRequest);
        Task UpdatePlanet(long planetId, UpdatePlanetRequest updatePlanetRequest);
        Task DeletePlanetById(long planetId);
    }
    public partial class PlanetService
    {
        private readonly IConfigurationProvider _mapper;
        private const int DefaultPaging = 8;
        private const int LimitPaging = 16;

        public PlanetService(IUnitOfWork unitOfWork, IPlanetRepository repository, IMapper mapper) : base(unitOfWork,
            repository)
        {
            _mapper = mapper.ConfigurationProvider;
        }

        public async Task<PageResult<PlanetModel>> GetAllPlanets(PlanetModel filter, string[] fields,
            string sort, int page, int limit)
        {
            var (total, queryable) = Get().ProjectTo<PlanetModel>(_mapper).DynamicFilter(filter).PagingIQueryable(page, limit, LimitPaging, DefaultPaging);
            if (sort != null)
            {
                queryable = queryable.OrderBy(sort);
            }

            if (fields.Length > 0)
            {
                queryable = queryable.Select<PlanetModel>(PlanetModel.Fields.Intersect(fields).ToArray()
                    .ToDynamicSelector<PlanetModel>());
            }

            return new PageResult<PlanetModel>
            {
                List = await queryable.ToListAsync(),
                Page = page,
                Limit = limit,
                Total = total
            };
        }

        public async Task<PlanetModel> GetPlanetById(long planetId)
        {
            var planetModel = await Get().ProjectTo<PlanetModel>(_mapper).FirstOrDefaultAsync(p => p.Id == planetId);
            if (planetModel == null)
            {
                throw new ErrorResponse((int)HttpStatusCode.NotFound,
                    $"Cannot find any planet matches with id = {planetId}");
            }

            return planetModel;
        }

        public async Task<long> CreatePlanet(CreatePlanetRequest createPlanetRequest)
        {
            var mapper = _mapper.CreateMapper();
            var planet = mapper.Map<Planet>(createPlanetRequest);
            await CreateAsyn(planet);
            return planet.Id;
        }

        public async Task UpdatePlanet(long planetId, UpdatePlanetRequest updatePlanetRequest)
        {
            var planetInDb = await Get().FirstOrDefaultAsync(p => p.Id == planetId);
            if (planetInDb == null)
            {
                throw new ErrorResponse((int)HttpStatusCode.NotFound,
                    $"Cannot find any planet matches with id = {planetId}");
            }
            
            var mapper = _mapper.CreateMapper();
            var planet = mapper.Map<Planet>(updatePlanetRequest);

            await UpdateAsyn(planet);
        }

        public async Task DeletePlanetById(long planetId)
        {
            var planet = await Get().FirstOrDefaultAsync(p => p.Id == planetId);
            if (planet == null)
            {
                throw new ErrorResponse((int)HttpStatusCode.NotFound,
                    $"Cannot find any planet matches with id = {planetId}");
            }

            await DeleteAsyn(planet);
        }
    }
}