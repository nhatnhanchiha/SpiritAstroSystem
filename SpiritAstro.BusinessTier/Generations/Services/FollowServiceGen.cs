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
    
    public partial interface IFollowService:IBaseService<Follow>
    {
    }
    public partial class FollowService:BaseService<Follow>,IFollowService
    {
        public FollowService(IUnitOfWork unitOfWork,IFollowRepository repository, IAccountService accountService, IAstrologerService astrologerService):base(unitOfWork,repository)
        {
            _astrologerService = astrologerService;
            _accountService = accountService;
        }
    }
}
