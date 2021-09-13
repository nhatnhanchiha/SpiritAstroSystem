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
    public partial interface ISubCategoryRepository :IBaseRepository<SubCategory>
    {
    }
    public partial class SubCategoryRepository :BaseRepository<SubCategory>, ISubCategoryRepository
    {
         public SubCategoryRepository(DbContext dbContext) : base(dbContext)
         {
         }
    }
}

