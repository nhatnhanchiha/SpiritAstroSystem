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
    
    public partial interface IFamousPersonService:IBaseService<FamousPerson>
    {
    }
    public partial class FamousPersonService:BaseService<FamousPerson>,IFamousPersonService
    {
        public FamousPersonService(IUnitOfWork unitOfWork,IFamousPersonRepository repository):base(unitOfWork,repository){}
    }
}
