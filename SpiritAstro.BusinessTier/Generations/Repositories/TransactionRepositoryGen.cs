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
    public partial interface ITransactionRepository :IBaseRepository<Transaction>
    {
    }
    public partial class TransactionRepository :BaseRepository<Transaction>, ITransactionRepository
    {
         public TransactionRepository(DbContext dbContext) : base(dbContext)
         {
         }
    }
}

