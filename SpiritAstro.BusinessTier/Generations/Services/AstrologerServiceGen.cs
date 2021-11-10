/////////////////////////////////////////////////////////////////
//
//              AUTO-GENERATED
//
/////////////////////////////////////////////////////////////////

using SpiritAstro.DataTier.BaseConnect;
using SpiritAstro.DataTier.Models;
using SpiritAstro.BusinessTier.Generations.Repositories;
using SpiritAstro.BusinessTier.Services;

namespace SpiritAstro.BusinessTier.Generations.Services
{
    
    public partial interface IAstrologerService:IBaseService<Astrologer>
    {
    }
    public partial class AstrologerService:BaseService<Astrologer>,IAstrologerService
    {
        public AstrologerService(IUnitOfWork unitOfWork,IAstrologerRepository repository, IAstroChartService astroChartService, IAstroOnlineService astroOnlineService):base(unitOfWork,repository)
        {
            _astroOnlineService = astroOnlineService;
            _astroChartService = astroChartService;
        }
    }
}
