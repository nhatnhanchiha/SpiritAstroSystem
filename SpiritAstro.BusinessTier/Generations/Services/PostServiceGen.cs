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
    
    public partial interface IPostService:IBaseService<Post>
    {
    }
    public partial class PostService:BaseService<Post>,IPostService
    {
        public PostService(IUnitOfWork unitOfWork,IPostRepository repository):base(unitOfWork,repository){}
    }
}
