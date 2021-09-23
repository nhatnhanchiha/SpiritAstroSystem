/////////////////////////////////////////////////////////////////
//
//              AUTO-GENERATED
//
/////////////////////////////////////////////////////////////////

using Microsoft.EntityFrameworkCore;
using SpiritAstro.DataTier.BaseConnect;
using SpiritAstro.DataTier.Models;
namespace SpiritAstro.BusinessTier.Generations.Repositories
{
    public partial interface IBookingRepository :IBaseRepository<Booking>
    {
    }
    public partial class BookingRepository :BaseRepository<Booking>, IBookingRepository
    {
         public BookingRepository(DbContext dbContext) : base(dbContext)
         {
         }
    }
}

