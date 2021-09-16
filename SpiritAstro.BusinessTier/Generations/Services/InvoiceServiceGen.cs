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
    
    public partial interface IInvoiceService:IBaseService<Invoice>
    {
    }
    public partial class InvoiceService:BaseService<Invoice>,IInvoiceService
    {
        public InvoiceService(IUnitOfWork unitOfWork,IInvoiceRepository repository):base(unitOfWork,repository){}
    }
}
