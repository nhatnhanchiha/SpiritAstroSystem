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
    
    public partial interface IBookingService:IBaseService<Booking>
    {
    }
    public partial class BookingService:BaseService<Booking>,IBookingService
    {
        public BookingService(IUnitOfWork unitOfWork,IBookingRepository repository):base(unitOfWork,repository){}
    }
}
