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
    public partial interface IFieldDetailRepository :IBaseRepository<FieldDetail>
    {
    }
    public partial class FieldDetailRepository :BaseRepository<FieldDetail>, IFieldDetailRepository
    {
         public FieldDetailRepository(DbContext dbContext) : base(dbContext)
         {
         }
    }
}

