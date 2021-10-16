using System.Linq;
using System.Net;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using SpiritAstro.BusinessTier.Commons.Utils;
using SpiritAstro.BusinessTier.Generations.Repositories;
using SpiritAstro.BusinessTier.Responses;
using SpiritAstro.BusinessTier.Services;
using SpiritAstro.BusinessTier.ViewModels.Follow;
using SpiritAstro.DataTier.BaseConnect;
using SpiritAstro.DataTier.Models;

namespace SpiritAstro.BusinessTier.Generations.Services
{
    public partial interface IFollowService
    {
        Task<PageResult<FollowWithAstrologer>> GetFollowings(long customerId, int page, int limit);
        Task<PageResult<FollowWithCustomer>> GetFollowers(long astrologerId, int page, int limit);
        Task Follow(long customerId, long astrologerId);
        Task Unfollow(long customerId, long astrologerId);

        Task<PageResult<FollowModel>> GetFollowList(FollowModel filter, string[] fields,
            string sort, int page, int limit);
    }
    public partial class FollowService
    {
        private readonly IConfigurationProvider _mapper;
        private readonly IAstrologerService _astrologerService;
        private const int LimitPaging = 50;
        private const int DefaultPaging = 20;
        

        public FollowService(IUnitOfWork unitOfWork, IFollowRepository repository, IMapper mapper, IAccountService accountService, IAstrologerService astrologerService) : base(unitOfWork,
            repository)
        {
            _astrologerService = astrologerService;
            _mapper = mapper.ConfigurationProvider;
        }

        public async Task<PageResult<FollowWithAstrologer>> GetFollowings(long customerId, int page, int limit)
        {
            var (total, queryable) = Get().ProjectTo<FollowWithAstrologer>(_mapper)
                .Where(f => f.CustomerId == customerId && f.Astrologer.DeletedAt == null).PagingIQueryable(page, limit, LimitPaging, DefaultPaging);
            return new PageResult<FollowWithAstrologer>
            {
                List = await queryable.ToListAsync(),
                Page = page,
                Limit = limit,
                Total = total
            };
        }

        public async Task<PageResult<FollowWithCustomer>> GetFollowers(long astrologerId, int page, int limit)
        {
            
            var (total, queryable) = Get().ProjectTo<FollowWithCustomer>(_mapper)
                .Where(f => f.AstrologerId == astrologerId && f.Customer.DeletedAt == null)
                .PagingIQueryable(page, limit, LimitPaging, DefaultPaging);
            
            return new PageResult<FollowWithCustomer>
            {
                List = await queryable.ToListAsync(),
                Page = page,
                Limit = limit,
                Total = total
            };
        }

        public async Task Follow(long customerId, long astrologerId)
        {
            await _astrologerService.IsAstrologer(astrologerId);
            
            var follow = await Get().FirstOrDefaultAsync(f => f.CustomerId == customerId && f.AstrologerId == astrologerId);
            if (follow != null)
            {
                throw new ErrorResponse((int)HttpStatusCode.BadRequest, "You has been follow this person");
            }

            await CreateAsyn(new Follow { CustomerId = customerId, AstrologerId = astrologerId });
        }

        public async Task Unfollow(long customerId, long astrologerId)
        {
            await _astrologerService.IsAstrologer(astrologerId);
            
            var follow = await Get().FirstOrDefaultAsync(f => f.CustomerId == customerId && f.AstrologerId == astrologerId);
            if (follow == null)
            {
                throw new ErrorResponse((int)HttpStatusCode.BadRequest, "You hasn't been follow this person");
            }

            await DeleteAsyn(follow);
        }

        public async Task<PageResult<FollowModel>> GetFollowList(FollowModel filter, string[] fields, string sort, int page, int limit)
        {
            var (total, queryable) = Get().ProjectTo<FollowModel>(_mapper).DynamicFilter(filter)
                .PagingIQueryable(page, limit, LimitPaging, DefaultPaging);
            if (sort != null)
            {
                queryable = queryable.OrderBy(sort);
            }

            if (fields.Length > 0)
            {
                queryable = queryable.Select<FollowModel>(FollowModel.Fields.Intersect(fields).ToArray()
                    .ToDynamicSelector<FollowModel>());
            }

            return new PageResult<FollowModel>
            {
                List = await queryable.ToListAsync(),
                Page = page,
                Limit = limit,
                Total = total
            };
        }
    }
}