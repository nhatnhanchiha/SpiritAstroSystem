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
    
    public partial interface IFieldDetailService:IBaseService<FieldDetail>
    {
    }
    public partial class FieldDetailService:BaseService<FieldDetail>,IFieldDetailService
    {
        public FieldDetailService(IUnitOfWork unitOfWork,IFieldDetailRepository repository):base(unitOfWork,repository){}
    }
}
