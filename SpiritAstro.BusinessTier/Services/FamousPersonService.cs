using AutoMapper;
using AutoMapper.Configuration;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using SpiritAstro.BusinessTier.Commons.Utils;
using SpiritAstro.BusinessTier.Generations.Repositories;
using SpiritAstro.BusinessTier.Requests.FamousPerson;
using SpiritAstro.BusinessTier.Responses;
using SpiritAstro.BusinessTier.ViewModels.FamousPerson;
using SpiritAstro.DataTier.BaseConnect;
using SpiritAstro.DataTier.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SpiritAstro.BusinessTier.Generations.Services
{
    public partial interface IFamousPersonService
    {
        Task<PageResult<FamousPersonModel>> GetListFamousPerson(FamousPersonModel famousPersonFilter, int page, int limit, string sort, string[] fields);
        Task<FamousPersonModel> GetFamousPersonById(long famousPersonId);

        Task<long> CreateFamousPerson(CreateFamousPersonRequest createFamousPersonRequest);

        Task UpdateFamousPerson(long famousPersonId, UpdateFamousPersonRequest updateFamousPersonRequest);

       Task DeleteFamousPerson(long famousPersonId);
    }

    public partial class FamousPersonService
    {
        private readonly IConfigurationProvider _mapper;
        private const int DefaultPaging = 10;
        private const int LimitPaging = 50;
        public FamousPersonService(IUnitOfWork unitOfWork, IFamousPersonRepository repository, IMapper mapper) : base(unitOfWork, repository) {
            _mapper = mapper.ConfigurationProvider;
        }

        public async Task<PageResult<FamousPersonModel>> GetListFamousPerson(FamousPersonModel famousPersonFilter, int page, int limit, string sort, string[] fields)
        {
            var (total, queryable) = Get().ProjectTo<FamousPersonModel>(_mapper)
                 .DynamicFilter(famousPersonFilter).PagingIQueryable(page, limit, LimitPaging, DefaultPaging);
            if (sort != null)
            {
                queryable = queryable.OrderBy(sort);
            }
            if (fields.Length > 0)
            {
                queryable = queryable.Select<FamousPersonModel>(FamousPersonModel.Fields.Intersect(fields).ToArray()
                    .ToDynamicSelector<FamousPersonModel>());
            }
            return new PageResult<FamousPersonModel>
            {
                List = await queryable.ToListAsync(),
                Page = page,
                Limit = limit,
                Total = total,
            };
        }
        public async Task<FamousPersonModel> GetFamousPersonById(long famousPersonId)
        {
            var famousPersonModel = await Get().Where(fp => fp.Id == famousPersonId).ProjectTo<FamousPersonModel>(_mapper).FirstOrDefaultAsync();
            if (famousPersonModel == null)
            {
                throw new ErrorResponse((int)HttpStatusCode.NotFound, $"Cannot find any famous person matches with id = {famousPersonId}");
            }
            return famousPersonModel;
        }

        public async Task<long> CreateFamousPerson(CreateFamousPersonRequest createFamousPersonRequest)
        {
            var mapper = _mapper.CreateMapper();
            var famousPerson = mapper.Map<FamousPerson>(createFamousPersonRequest);
            await CreateAsyn(famousPerson);
            return famousPerson.Id;
        }

        public async Task UpdateFamousPerson(long famousPersonId, UpdateFamousPersonRequest updateFamousPersonRequest)
        {
           var famousPersonInDb = await Get().FirstOrDefaultAsync(fp => fp.Id == famousPersonId);
            if (famousPersonInDb == null)
            {
                throw new ErrorResponse((int)HttpStatusCode.NotFound, $"Cannot find any famous person matches with id = {famousPersonId}");
            }

            var mapper = _mapper.CreateMapper();
            var famousPersonInRequest = mapper.Map<FamousPerson>(updateFamousPersonRequest);

            famousPersonInDb.Name = famousPersonInRequest.Name;
            famousPersonInDb.Description = famousPersonInRequest.Description;
            famousPersonInDb.Zodiac = famousPersonInRequest.Zodiac;

            await UpdateAsyn(famousPersonInDb);
        }

        public async Task DeleteFamousPerson(long famousPersonId)
        {
            var categoryInDb = await Get().FirstOrDefaultAsync(fp => fp.Id == famousPersonId);
            if (categoryInDb == null)
            {
                throw new ErrorResponse((int)HttpStatusCode.NotFound,
                    $"Cannot find any category matches with id = {famousPersonId}");
            }

            await DeleteAsyn(categoryInDb);
        }
    }
}
