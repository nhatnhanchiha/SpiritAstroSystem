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
    
    public partial interface IUserService:IBaseService<User>
    {
    }
    public partial class UserService:BaseService<User>,IUserService
    {
        public UserService(IUnitOfWork unitOfWork,IUserRepository repository):base(unitOfWork,repository)
        {
        }
    }
}
