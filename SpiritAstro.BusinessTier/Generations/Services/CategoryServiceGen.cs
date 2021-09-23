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
    
    public partial interface ICategoryService:IBaseService<Category>
    {
    }
    public partial class CategoryService:BaseService<Category>,ICategoryService
    {
        public CategoryService(IUnitOfWork unitOfWork,ICategoryRepository repository):base(unitOfWork,repository){}
    }
}
