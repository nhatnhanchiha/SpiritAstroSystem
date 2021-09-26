/////////////////////////////////////////////////////////////////
//
//              AUTO-GENERATED
//
/////////////////////////////////////////////////////////////////

using SpiritAstro.DataTier.BaseConnect;
using SpiritAstro.DataTier.Models;
using SpiritAstro.BusinessTier.Generations.Repositories;
using SpiritAstro.BusinessTier.Services;

namespace SpiritAstro.BusinessTier.Generations.Services
{
    
    public partial interface ICustomerZodiacService:IBaseService<CustomerZodiac>
    {
    }
    public partial class CustomerZodiacService:BaseService<CustomerZodiac>,ICustomerZodiacService
    {
        public CustomerZodiacService(IUnitOfWork unitOfWork,ICustomerZodiacRepository repository, IAccountService accountService):base(unitOfWork,repository)
        {
            _accountService = accountService;
        }
    }
}
