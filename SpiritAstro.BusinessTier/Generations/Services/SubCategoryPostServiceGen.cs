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
    
    public partial interface ISubCategoryPostService:IBaseService<SubCategoryPost>
    {
    }
    public partial class SubCategoryPostService:BaseService<SubCategoryPost>,ISubCategoryPostService
    {
        public SubCategoryPostService(IUnitOfWork unitOfWork,ISubCategoryPostRepository repository):base(unitOfWork,repository){}
    }
}
