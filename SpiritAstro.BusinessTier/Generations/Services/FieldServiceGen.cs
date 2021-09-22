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
    
    public partial interface IFieldService:IBaseService<Field>
    {
    }
    public partial class FieldService:BaseService<Field>,IFieldService
    {
        public FieldService(IUnitOfWork unitOfWork,IFieldRepository repository):base(unitOfWork,repository){}
    }
}
