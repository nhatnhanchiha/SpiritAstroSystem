using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using SpiritAstro.BusinessTier.Generations.Repositories;
using SpiritAstro.BusinessTier.ViewModels.Zodiac;
using SpiritAstro.DataTier.BaseConnect;
using SpiritAstro.BusinessTier.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using SpiritAstro.BusinessTier.Requests.Zodiac;
using SpiritAstro.DataTier.Models;
using SpiritAstro.BusinessTier.Commons.Utils;
using System.Linq.Dynamic.Core;

namespace SpiritAstro.BusinessTier.Generations.Services
{
    public partial interface IZodiacService
    {
        Task<ZodiacModel> GetZodiacById(long zodiacId);
        Task<long> CreateZodiac(CreateZodiacRequest createZodiac);
        Task UpdateZodiac(long zodiacId, UpdateZodiacRequest updateZodiacRequest);
        Task<PageResult<ZodiacModel>> GetListZodiac(ZodiacModel zodiacFilter, string[] fields, string sort, int page, int limit);
    }
    public partial class ZodiacService
    {
        private readonly IConfigurationProvider _mapper;
        private const int DefaultPaging = 10;
        private const int LimitPaging = 50;
        public ZodiacService(IUnitOfWork unitOfWork, IZodiacRepository repository, IMapper mapper) : base(unitOfWork, repository)
        {
            _mapper = mapper.ConfigurationProvider;
        }
        public async Task<PageResult<ZodiacModel>> GetListZodiac(ZodiacModel zodiacFilter, string[] fields, string sort, int page, int limit)
        {
            var (total, queryable) = Get().ProjectTo<ZodiacModel>(_mapper).DynamicFilter(zodiacFilter).PagingIQueryable(page, limit, LimitPaging, DefaultPaging);
            if (sort != null)
            {
                queryable = queryable.OrderBy(sort);
            }

            if (fields.Length > 0)
            {
                queryable = queryable.Select<ZodiacModel>(ZodiacModel.Fields.Intersect(fields).ToArray()
                    .ToDynamicSelector<ZodiacModel>());
            }
            return new PageResult<ZodiacModel>
            {
                List = await queryable.ToListAsync(),
                Page = page,
                Limit = limit,
                Total = total,
            };
        }
        public async Task<ZodiacModel> GetZodiacById(long zodiacId)
        {
            var zodiacModel = await Get().Where(z => z.Id == zodiacId).ProjectTo<ZodiacModel>(_mapper).FirstOrDefaultAsync();
            if (zodiacModel == null)
            {
                throw new ErrorResponse((int)HttpStatusCode.NotFound, $"Cannot find any zodiac matches with id = {zodiacId}");
            }
            return zodiacModel;
        }

        public async Task<long> CreateZodiac(CreateZodiacRequest createZodiac)
        {
            var mapper = _mapper.CreateMapper();
            var z = mapper.Map<Zodiac>(createZodiac);
            await CreateAsyn(z);
            return z.Id;
        }

        public async Task UpdateZodiac(long zodiacId, UpdateZodiacRequest updateZodiacRequest)
        {
            var zodiacInDb = await Get().FirstOrDefaultAsync(z => z.Id == zodiacId);
            if (zodiacInDb == null)
            {
                throw new ErrorResponse((int)HttpStatusCode.NotFound, $"Cannot find any zodiac matches with id = {zodiacId}");
            }

            var mapper = _mapper.CreateMapper();
            var zodiacInRequest = mapper.Map<Zodiac>(updateZodiacRequest);

            zodiacInDb.Name = zodiacInRequest.Name;
            zodiacInDb.Description = zodiacInRequest.Description;
            

            await UpdateAsyn(zodiacInDb);
        }
    }
}
