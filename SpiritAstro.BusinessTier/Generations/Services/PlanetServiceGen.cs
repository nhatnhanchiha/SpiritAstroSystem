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
    
    public partial interface IPlanetService:IBaseService<Planet>
    {
    }
    public partial class PlanetService:BaseService<Planet>,IPlanetService
    {
        public PlanetService(IUnitOfWork unitOfWork,IPlanetRepository repository):base(unitOfWork,repository){}
    }
}
