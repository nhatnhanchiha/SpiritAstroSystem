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
    public partial interface IUserPostRepository :IBaseRepository<UserPost>
    {
    }
    public partial class UserPostRepository :BaseRepository<UserPost>, IUserPostRepository
    {
         public UserPostRepository(DbContext dbContext) : base(dbContext)
         {
         }
    }
}

