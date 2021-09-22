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
    
    public partial interface ICustomerZodiacService:IBaseService<CustomerZodiac>
    {
    }
    public partial class CustomerZodiacService:BaseService<CustomerZodiac>,ICustomerZodiacService
    {
        public CustomerZodiacService(IUnitOfWork unitOfWork,ICustomerZodiacRepository repository):base(unitOfWork,repository){}
    }
}
