using AutoMapper;
using SpiritAstro.BusinessTier.Generations.Repositories;
using SpiritAstro.BusinessTier.Requests.UserRole;
using SpiritAstro.BusinessTier.Responses;
using SpiritAstro.BusinessTier.ViewModels.UserRole;
using SpiritAstro.DataTier.BaseConnect;
using SpiritAstro.DataTier.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using AutoMapper.QueryableExtensions;
using SpiritAstro.BusinessTier.Commons.Utils;
using Microsoft.EntityFrameworkCore;

namespace SpiritAstro.BusinessTier.Generations.Services
{
    public partial interface IUserRoleService
    {
        Task<long> CreateUserRole(CreateUserRoleRequest createUserRoleZodiac);
        Task<PageResult<UserRoleModel>> GetListUserRole(UserRoleModel userRoleFilter, int page, int limit, string sort, string[] fields);
    }
    public partial class UserRoleService
    {
        private readonly IConfigurationProvider _mapper;
        private const int DefaultPaging = 10;
        private const int LimitPaging = 50;

        public UserRoleService(IUnitOfWork unitOfWork, IUserRoleRepository repository, IMapper mapper) : base(
            unitOfWork, repository)
        {
            _mapper = mapper.ConfigurationProvider;
        }

        public async Task<long> CreateUserRole(CreateUserRoleRequest createUserRoleZodiac)
        {
            var mapper = _mapper.CreateMapper();
            var ur = mapper.Map<UserRole>(createUserRoleZodiac);
            await CreateAsyn(ur);
            return ur.UserId;
        }

        public async Task<PageResult<UserRoleModel>> GetListUserRole(UserRoleModel userRoleFilter, int page, int limit, string sort, string[] fields)
        {
            var (total, queryable) = Get().ProjectTo<UserRoleModel>(_mapper)
                 .DynamicFilter(userRoleFilter).PagingIQueryable(page, limit, LimitPaging, DefaultPaging);
            if (sort != null)
            {
                queryable = queryable.OrderBy(sort);
            }
            if (fields.Length > 0)
            {
                queryable = queryable.Select<UserRoleModel>(UserRoleModel.Fields.Intersect(fields).ToArray()
                    .ToDynamicSelector<UserRoleModel>());
            }
            return new PageResult<UserRoleModel>
            {
                List = await queryable.ToListAsync(),
                Page = page,
                Limit = limit,
                Total = total,
            };
        }
    }
}
