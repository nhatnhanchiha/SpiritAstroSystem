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
    public partial interface IZodiacRepository :IBaseRepository<Zodiac>
    {
    }
    public partial class ZodiacRepository :BaseRepository<Zodiac>, IZodiacRepository
    {
         public ZodiacRepository(DbContext dbContext) : base(dbContext)
         {
         }
    }
}

