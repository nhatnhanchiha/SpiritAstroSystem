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
    
    public partial interface IBookingService:IBaseService<Booking>
    {
    }
    public partial class BookingService:BaseService<Booking>,IBookingService
    {
        public BookingService(IUnitOfWork unitOfWork,IBookingRepository repository, IAccountService accountService):base(unitOfWork,repository)
        {
            _accountService = accountService;
        }
    }
}
