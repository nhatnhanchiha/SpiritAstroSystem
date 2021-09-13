/////////////////////////////////////////////////////////////////
//
//              AUTO-GENERATED
//
/////////////////////////////////////////////////////////////////

using SpiritAstro.DataTier.BaseConnect;
using SpiritAstro.DataTier.Models;
using SpiritAstro.BusinessTier.Generations.Repositories;
using Test.DataTier.BaseConnect;

namespace SpiritAstro.BusinessTier.Generations.Services
{
    
    public partial interface IUserRoleService:IBaseService<UserRole>
    {
    }
    public partial class UserRoleService:BaseService<UserRole>,IUserRoleService
    {
        public UserRoleService(IUnitOfWork unitOfWork,IUserRoleRepository repository):base(unitOfWork,repository){}
    }
}
