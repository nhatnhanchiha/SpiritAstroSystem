using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using SpiritAstro.BusinessTier.Commons.Utils;
using SpiritAstro.BusinessTier.Generations.Repositories;
using SpiritAstro.BusinessTier.Responses;
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
    }
    public partial class FollowService
    {
        private readonly IConfigurationProvider _mapper;
        private const int LimitPaging = 50;
        private const int DefaultPaging = 20;
        

        public FollowService(IUnitOfWork unitOfWork, IFollowRepository repository, IMapper mapper) : base(unitOfWork,
            repository)
        {
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
                Size = limit,
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
                Size = limit,
                Total = total
            };
        }

        public async Task Follow(long customerId, long astrologerId)
        {
            var follow = await Get().FirstOrDefaultAsync(f => f.CustomerId == customerId && f.AstrologerId == astrologerId);
            if (follow != null)
            {
                throw new ErrorResponse((int)HttpStatusCode.BadRequest, "You has been follow this person");
            }

            await CreateAsyn(new Follow { CustomerId = customerId, AstrologerId = astrologerId });
        }
    }
}