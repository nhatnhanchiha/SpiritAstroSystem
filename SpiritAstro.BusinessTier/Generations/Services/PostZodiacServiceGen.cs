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
    
    public partial interface IPostZodiacService:IBaseService<PostZodiac>
    {
    }
    public partial class PostZodiacService:BaseService<PostZodiac>,IPostZodiacService
    {
        public PostZodiacService(IUnitOfWork unitOfWork,IPostZodiacRepository repository):base(unitOfWork,repository){}
    }
}
