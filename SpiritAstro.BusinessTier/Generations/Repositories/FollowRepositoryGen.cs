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
    public partial interface IFollowRepository :IBaseRepository<Follow>
    {
    }
    public partial class FollowRepository :BaseRepository<Follow>, IFollowRepository
    {
         public FollowRepository(DbContext dbContext) : base(dbContext)
         {
         }
    }
}

