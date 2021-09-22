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
    
    public partial interface IPostPlanetService:IBaseService<PostPlanet>
    {
    }
    public partial class PostPlanetService:BaseService<PostPlanet>,IPostPlanetService
    {
        public PostPlanetService(IUnitOfWork unitOfWork,IPostPlanetRepository repository):base(unitOfWork,repository){}
    }
}
