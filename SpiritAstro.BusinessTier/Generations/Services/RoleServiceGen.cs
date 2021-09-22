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
    
    public partial interface IRoleService:IBaseService<Role>
    {
    }
    public partial class RoleService:BaseService<Role>,IRoleService
    {
        public RoleService(IUnitOfWork unitOfWork,IRoleRepository repository):base(unitOfWork,repository){}
    }
}
