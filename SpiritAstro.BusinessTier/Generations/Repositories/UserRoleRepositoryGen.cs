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
    public partial interface IUserRoleRepository :IBaseRepository<UserRole>
    {
    }
    public partial class UserRoleRepository :BaseRepository<UserRole>, IUserRoleRepository
    {
         public UserRoleRepository(DbContext dbContext) : base(dbContext)
         {
         }
    }
}

