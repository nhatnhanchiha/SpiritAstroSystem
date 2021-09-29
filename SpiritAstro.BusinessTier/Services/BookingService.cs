using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using SpiritAstro.BusinessTier.Commons.Enums.Booking;
using SpiritAstro.BusinessTier.Commons.Utils;
using SpiritAstro.BusinessTier.Generations.Repositories;
using SpiritAstro.BusinessTier.Requests.Booking;
using SpiritAstro.BusinessTier.Responses;
using SpiritAstro.BusinessTier.Services;
using SpiritAstro.BusinessTier.ViewModels.Booking;
using SpiritAstro.DataTier.BaseConnect;
using SpiritAstro.DataTier.Models;
using System.Linq.Dynamic.Core;

namespace SpiritAstro.BusinessTier.Generations.Services
{
    public partial interface IBookingService
    {
        Task<BookingModel> GetBookingById(long bookingId);
        Task<long> CreateBooking(CreateBookingRequest createBookingRequest);
        Task<PageResult<BookingModel>> GetListBookings(BookingModel bookingFilter, string[] fields, string sort, int page, int limit);
    }

    public partial class BookingService
    {
        private readonly IConfigurationProvider _mapper;
        private readonly IAccountService _accountService;
        private const int DefaultPaging = 10;
        private const int LimitPaging = 50;

        public BookingService(IUnitOfWork unitOfWork, IBookingRepository repository, IMapper mapper, IAccountService accountService) : base(unitOfWork,
            repository)
        {
            _accountService = accountService;
            _mapper = mapper.ConfigurationProvider;
        }

        public async Task<PageResult<BookingModel>> GetListBookings(BookingModel bookingFilter, string[] fields, string sort, int page, int limit)
        {
            var (total, queryable) = Get().ProjectTo<BookingModel>(_mapper).DynamicFilter(bookingFilter)
                .PagingIQueryable(page, limit, LimitPaging, DefaultPaging);
            if (sort != null)
            {
                queryable = queryable.OrderBy(sort);
            }
            if (fields.Length > 0)
            {
                queryable = queryable.Select<BookingModel>(BookingModel.Fields.Intersect(fields).ToArray()
                    .ToDynamicSelector<BookingModel>());
            }
            return new PageResult<BookingModel>
            {
                List = await queryable.ToListAsync(),
                Page = page,
                Limit = limit,
                Total = total
            };
        }

        public async Task<BookingModel> GetBookingById(long bookingId)
        {
            var bookingModel = await Get().ProjectTo<BookingModel>(_mapper).FirstOrDefaultAsync(b => b.Id == bookingId);
            if (bookingModel == null)
            {
                throw new ErrorResponse((int)HttpStatusCode.NotFound,
                    $"cannot find any booking matches with id = {bookingId}");
            }

            return bookingModel;
        }

        public async Task<long> CreateBooking(CreateBookingRequest createBookingRequest)
        {
            var mapper = _mapper.CreateMapper();
            var booking = mapper.Map<Booking>(createBookingRequest);
            if (booking.StartTime < DateTimeOffset.Now || booking.StartTime >= booking.EndTime)
            {
                throw new ErrorResponse((int)HttpStatusCode.BadRequest,
                    "Invalid time");
            }

            if ((booking.EndTime - booking.StartTime).TotalMinutes < 10)
            {
                throw new ErrorResponse((int)HttpStatusCode.BadRequest,
                    "During time must be greater than 10 minutes");
            }
            
            var customerId = _accountService.GetCustomerId();

            var otherBooking = await Get().FirstOrDefaultAsync(b => b.CustomerId == customerId && (b.StartTime <= booking.StartTime && b.EndTime >= booking.StartTime || b.StartTime <= booking.EndTime && b.EndTime >= booking.EndTime));
            if (otherBooking != null)
            {
                throw new ErrorResponse((int)HttpStatusCode.BadRequest,
                    "You has a booking in this time, please choose another time!");
            }

            booking.CustomerId = customerId;

            var bookingInDb = await Get().FirstOrDefaultAsync(b =>
                b.CustomerId == customerId && b.AstrologerId == booking.AstrologerId &&
                b.Status != (int)BookingStatus.Cancel);

            if (bookingInDb != null)
            {
                throw new ErrorResponse((int)HttpStatusCode.BadRequest,
                    $"You made the booking with the astrologer has id = {booking.AstrologerId}");
            }

            await CreateAsyn(booking);

            return booking.Id;
        }
    }
}