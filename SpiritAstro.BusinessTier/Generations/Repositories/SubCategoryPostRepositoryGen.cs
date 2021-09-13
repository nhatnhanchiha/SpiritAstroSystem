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
    public partial interface ISubCategoryPostRepository :IBaseRepository<SubCategoryPost>
    {
    }
    public partial class SubCategoryPostRepository :BaseRepository<SubCategoryPost>, ISubCategoryPostRepository
    {
         public SubCategoryPostRepository(DbContext dbContext) : base(dbContext)
         {
         }
    }
}

