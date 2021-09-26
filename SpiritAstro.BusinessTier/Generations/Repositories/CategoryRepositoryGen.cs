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
    public partial interface ICategoryRepository :IBaseRepository<Category>
    {
    }
    public partial class CategoryRepository :BaseRepository<Category>, ICategoryRepository
    {
         public CategoryRepository(DbContext dbContext) : base(dbContext)
         {
         }
    }
}

