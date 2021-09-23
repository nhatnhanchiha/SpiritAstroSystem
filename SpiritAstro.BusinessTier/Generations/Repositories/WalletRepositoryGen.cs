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
    public partial interface IWalletRepository :IBaseRepository<Wallet>
    {
    }
    public partial class WalletRepository :BaseRepository<Wallet>, IWalletRepository
    {
         public WalletRepository(DbContext dbContext) : base(dbContext)
         {
         }
    }
}

