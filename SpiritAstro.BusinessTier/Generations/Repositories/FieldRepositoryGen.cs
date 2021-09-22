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
    public partial interface IFieldRepository :IBaseRepository<Field>
    {
    }
    public partial class FieldRepository :BaseRepository<Field>, IFieldRepository
    {
         public FieldRepository(DbContext dbContext) : base(dbContext)
         {
         }
    }
}

