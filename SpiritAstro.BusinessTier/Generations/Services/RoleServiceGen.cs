/////////////////////////////////////////////////////////////////
//
//              AUTO-GENERATED
//
/////////////////////////////////////////////////////////////////

using SpiritAstro.DataTier.Models;
using SpiritAstro.BusinessTier.Generations.Repositories;
using Test.DataTier.BaseConnect;

namespace SpiritAstro.BusinessTier.Generations.Services
{
    
    public partial interface IRoleService:IBaseService<Role>
    {
    }
    public partial class RoleService:BaseService<Role>,IRoleService
    {
        public RoleService(IUnitOfWork unitOfWork,IRoleRepository repository):base(unitOfWork,repository){}
    }
}
