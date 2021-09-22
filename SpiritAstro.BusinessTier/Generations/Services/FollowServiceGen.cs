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
    
    public partial interface IFollowService:IBaseService<Follow>
    {
    }
    public partial class FollowService:BaseService<Follow>,IFollowService
    {
        public FollowService(IUnitOfWork unitOfWork,IFollowRepository repository):base(unitOfWork,repository){}
    }
}
