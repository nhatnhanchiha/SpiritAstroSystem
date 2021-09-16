/////////////////////////////////////////////////////////////////
//
//              AUTO-GENERATED
//
/////////////////////////////////////////////////////////////////

using SpiritAstro.DataTier.Models;
using SpiritAstro.BusinessTier.Generations.Repositories;
using SpiritAstro.DataTier.BaseConnect;

namespace SpiritAstro.BusinessTier.Generations.Services
{
    
    public partial interface IUserPostService:IBaseService<UserPost>
    {
    }
    public partial class UserPostService:BaseService<UserPost>,IUserPostService
    {
        public UserPostService(IUnitOfWork unitOfWork,IUserPostRepository repository):base(unitOfWork,repository){}
    }
}
