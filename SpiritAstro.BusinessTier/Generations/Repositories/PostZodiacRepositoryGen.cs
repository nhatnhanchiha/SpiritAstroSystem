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
    public partial interface IPostZodiacRepository :IBaseRepository<PostZodiac>
    {
    }
    public partial class PostZodiacRepository :BaseRepository<PostZodiac>, IPostZodiacRepository
    {
         public PostZodiacRepository(DbContext dbContext) : base(dbContext)
         {
         }
    }
}

