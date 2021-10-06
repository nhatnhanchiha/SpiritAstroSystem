using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using SpiritAstro.BusinessTier.Commons.Utils;
using SpiritAstro.BusinessTier.Generations.Repositories;
using SpiritAstro.BusinessTier.Requests.PostZodiac;
using SpiritAstro.BusinessTier.Responses;
using SpiritAstro.BusinessTier.ViewModels.PostZodiac;
using SpiritAstro.DataTier.BaseConnect;
using SpiritAstro.DataTier.Models;
using System.Linq.Dynamic.Core;

namespace SpiritAstro.BusinessTier.Generations.Services
{
    public partial interface IPostZodiacService
    {
        Task<long> CreatePostZodiac(CreatePostZodiacRequest createPostZodiac);
        Task<PageResult<PostZodiacModel>> GetListPostZodiac(PostZodiacModel postZodiacFilter, int page, int limit, string sort, string[] fields);
    }

    public partial class PostZodiacService
    {
        private readonly IConfigurationProvider _mapper;
        private const int DefaultPaging = 10;
        private const int LimitPaging = 50;

        public PostZodiacService(IUnitOfWork unitOfWork, IPostZodiacRepository repository, IMapper mapper) : base(
            unitOfWork, repository)
        {
            _mapper = mapper.ConfigurationProvider;
        }

        public async Task<long> CreatePostZodiac(CreatePostZodiacRequest createPostZodiac)
        {
            var mapper = _mapper.CreateMapper();
            var pz = mapper.Map<PostZodiac>(createPostZodiac);
            await CreateAsyn(pz);
            return pz.PostId;
        }

        public async Task<PageResult<PostZodiacModel>> GetListPostZodiac(PostZodiacModel postZodiacFilter, int page, int limit, string sort, string[] fields)
        {
            var (total, queryable) = Get().ProjectTo<PostZodiacModel>(_mapper)
                 .DynamicFilter(postZodiacFilter).PagingIQueryable(page, limit, LimitPaging, DefaultPaging);
            if (sort != null)
            {
                queryable = queryable.OrderBy(sort);
            }
            if (fields.Length > 0)
            {
                queryable = queryable.Select<PostZodiacModel>(PostZodiacModel.Fields.Intersect(fields).ToArray()
                    .ToDynamicSelector<PostZodiacModel>());
            }
            return new PageResult<PostZodiacModel>
            {
                List = await queryable.ToListAsync(),
                Page = page,
                Limit = limit,
                Total = total,
            };
        }
    }
}
