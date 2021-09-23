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
    public partial interface IUserRepository :IBaseRepository<User>
    {
    }
    public partial class UserRepository :BaseRepository<User>, IUserRepository
    {
         public UserRepository(DbContext dbContext) : base(dbContext)
         {
         }
    }
}

