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
    
    public partial interface IPostService:IBaseService<Post>
    {
    }
    public partial class PostService:BaseService<Post>,IPostService
    {
        public PostService(IUnitOfWork unitOfWork,IPostRepository repository, IAccountService accountService):base(unitOfWork,repository)
        {
            _accountService = accountService;
        }
    }
}
