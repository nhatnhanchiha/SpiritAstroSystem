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
    
    public partial interface IPaymentService:IBaseService<Payment>
    {
    }
    public partial class PaymentService:BaseService<Payment>,IPaymentService
    {
        public PaymentService(IUnitOfWork unitOfWork,IPaymentRepository repository):base(unitOfWork,repository){}
    }
}
