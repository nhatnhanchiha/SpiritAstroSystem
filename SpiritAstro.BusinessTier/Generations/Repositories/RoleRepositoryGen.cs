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
    public partial interface IRoleRepository :IBaseRepository<Role>
    {
    }
    public partial class RoleRepository :BaseRepository<Role>, IRoleRepository
    {
         public RoleRepository(DbContext dbContext) : base(dbContext)
         {
         }
    }
}

