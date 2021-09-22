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
    
    public partial interface IPriceTableService:IBaseService<PriceTable>
    {
    }
    public partial class PriceTableService:BaseService<PriceTable>,IPriceTableService
    {
        public PriceTableService(IUnitOfWork unitOfWork,IPriceTableRepository repository):base(unitOfWork,repository){}
    }
}
