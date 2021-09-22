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
    
    public partial interface IZodiacService:IBaseService<Zodiac>
    {
    }
    public partial class ZodiacService:BaseService<Zodiac>,IZodiacService
    {
        public ZodiacService(IUnitOfWork unitOfWork,IZodiacRepository repository):base(unitOfWork,repository){}
    }
}
