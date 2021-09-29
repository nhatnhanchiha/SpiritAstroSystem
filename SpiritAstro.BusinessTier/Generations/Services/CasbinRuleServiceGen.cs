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
    
    public partial interface ICasbinRuleService:IBaseService<CasbinRule>
    {
    }
    public partial class CasbinRuleService:BaseService<CasbinRule>,ICasbinRuleService
    {
        public CasbinRuleService(IUnitOfWork unitOfWork,ICasbinRuleRepository repository):base(unitOfWork,repository){}
    }
}
