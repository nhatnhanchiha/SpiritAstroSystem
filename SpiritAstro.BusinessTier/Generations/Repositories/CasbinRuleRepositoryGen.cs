/////////////////////////////////////////////////////////////////
//
//              AUTO-GENERATED
//
/////////////////////////////////////////////////////////////////

using Microsoft.EntityFrameworkCore;
using SpiritAstro.DataTier.BaseConnect;
using SpiritAstro.DataTier.Models;
namespace SpiritAstro.BusinessTier.Generations.Repositories
{
    public partial interface ICasbinRuleRepository :IBaseRepository<CasbinRule>
    {
    }
    public partial class CasbinRuleRepository :BaseRepository<CasbinRule>, ICasbinRuleRepository
    {
         public CasbinRuleRepository(DbContext dbContext) : base(dbContext)
         {
         }
    }
}

