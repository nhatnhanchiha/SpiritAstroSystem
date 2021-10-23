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
    
    public partial interface IAstrologerService:IBaseService<Astrologer>
    {
    }
    public partial class AstrologerService:BaseService<Astrologer>,IAstrologerService
    {
        public AstrologerService(IUnitOfWork unitOfWork,IAstrologerRepository repository):base(unitOfWork,repository){}
    }
}
