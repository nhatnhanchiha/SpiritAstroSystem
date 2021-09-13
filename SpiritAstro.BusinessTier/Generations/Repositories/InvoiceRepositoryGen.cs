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
    public partial interface IInvoiceRepository :IBaseRepository<Invoice>
    {
    }
    public partial class InvoiceRepository :BaseRepository<Invoice>, IInvoiceRepository
    {
         public InvoiceRepository(DbContext dbContext) : base(dbContext)
         {
         }
    }
}

