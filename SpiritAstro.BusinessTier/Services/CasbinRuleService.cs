using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SpiritAstro.BusinessTier.Commons.Utils;
using SpiritAstro.BusinessTier.Responses;
using SpiritAstro.DataTier.Models;

namespace SpiritAstro.BusinessTier.Generations.Services
{
    public partial interface ICasbinRuleService
    {
        Task<PageResult<CasbinRule>> GetCasbinRules(int page, int limit);
    }
    public partial class CasbinRuleService
    {
        private const int DefaultPaging = 20;
        private const int LimitPaging = 20;
        public async Task<PageResult<CasbinRule>> GetCasbinRules(int page, int limit)
        {
            var (total, queryable) = Get().PagingIQueryable(page, limit, LimitPaging, DefaultPaging);

            return new PageResult<CasbinRule>
            {
                List = await queryable.ToListAsync(),
                Limit = limit,
                Page = page,
                Total = total,
            };
        } 
    }
}