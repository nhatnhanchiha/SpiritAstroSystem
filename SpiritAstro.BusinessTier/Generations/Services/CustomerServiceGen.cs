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
    
    public partial interface ICustomerService:IBaseService<Customer>
    {
    }
    public partial class CustomerService:BaseService<Customer>,ICustomerService
    {
        public CustomerService(IUnitOfWork unitOfWork,ICustomerRepository repository):base(unitOfWork,repository){}
    }
}
