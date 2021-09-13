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
    
    public partial interface ISubCategoryService:IBaseService<SubCategory>
    {
    }
    public partial class SubCategoryService:BaseService<SubCategory>,ISubCategoryService
    {
        public SubCategoryService(IUnitOfWork unitOfWork,ISubCategoryRepository repository):base(unitOfWork,repository){}
    }
}
