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
    public partial interface IPriceTableRepository :IBaseRepository<PriceTable>
    {
    }
    public partial class PriceTableRepository :BaseRepository<PriceTable>, IPriceTableRepository
    {
         public PriceTableRepository(DbContext dbContext) : base(dbContext)
         {
         }
    }
}

