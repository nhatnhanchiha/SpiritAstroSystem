/////////////////////////////////////////////////////////////////
//
//              AUTO-GENERATED
//
/////////////////////////////////////////////////////////////////

using SpiritAstro.DataTier.BaseConnect;
using SpiritAstro.DataTier.Models;
using SpiritAstro.BusinessTier.Generations.Repositories;
namespace SpiritAstro.BusinessTier.Generations.Services
{
    
    public partial interface IWalletService:IBaseService<Wallet>
    {
    }
    public partial class WalletService:BaseService<Wallet>,IWalletService
    {
        public WalletService(IUnitOfWork unitOfWork,IWalletRepository repository):base(unitOfWork,repository){}
    }
}
