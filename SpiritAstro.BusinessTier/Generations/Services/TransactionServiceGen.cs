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
    
    public partial interface ITransactionService:IBaseService<Transaction>
    {
    }
    public partial class TransactionService:BaseService<Transaction>,ITransactionService
    {
        public TransactionService(IUnitOfWork unitOfWork,ITransactionRepository repository):base(unitOfWork,repository){}
    }
}
