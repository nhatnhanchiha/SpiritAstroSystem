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
    public partial interface IPostRepository :IBaseRepository<Post>
    {
    }
    public partial class PostRepository :BaseRepository<Post>, IPostRepository
    {
         public PostRepository(DbContext dbContext) : base(dbContext)
         {
         }
    }
}

