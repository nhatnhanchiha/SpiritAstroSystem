using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpiritAstro.BusinessTier.Entities;
using SpiritAstro.BusinessTier.Generations.Services;
using SpiritAstro.BusinessTier.Requests.Booking;
using SpiritAstro.BusinessTier.Responses;
using SpiritAstro.BusinessTier.ViewModels.Booking;

namespace SpiritAstro.WebApi.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly IBookingService _bookingService;

        public BookingsController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        [HttpGet]
        public async Task<IActionResult> GetListBookings([FromQuery] BookingModel bookingFilter,[FromQuery] string[] fields, string sort, int page, int limit)
        {
            try
            {
                var categoryModels = await _bookingService.GetListBookings(bookingFilter, fields, sort, page, limit);
                return Ok(MyResponse<PageResult<BookingModel>>.OkWithData(categoryModels));
            }
            catch (ErrorResponse e)
            {
                return e.Error.Code switch
                {
                    _ => Ok(MyResponse<object>.FailWithMessage(e.Error.Message))
                };
            }
        }

        // Chả biết để làm gì
        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetBookingById(long id)
        {
            try
            {
                var bookingModel = await _bookingService.GetBookingById(id);
                return Ok(MyResponse<BookingModel>.OkWithData(bookingModel));
            }
            catch (ErrorResponse e)
            {
                return e.Error.Code switch
                {
                    (int)HttpStatusCode.NotFound => Ok(MyResponse<object>.FailWithMessage(e.Error.Message)),
                    _ => Ok(MyResponse<object>.FailWithMessage(e.Error.Message))
                };
            }
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateBooking(CreateBookingRequest createBookingRequest)
        {
            try
            {
                var bookingId = await _bookingService.CreateBooking(createBookingRequest);
                return Ok(MyResponse<long>.OkWithDetail(bookingId, $"Created success with bookingId = {bookingId}"));
            }
            catch (ErrorResponse e)
            {
                return e.Error.Code switch
                {
                    (int)HttpStatusCode.NotFound => Ok(MyResponse<object>.FailWithMessage(e.Error.Message)),
                    (int)HttpStatusCode.BadRequest => Ok(MyResponse<object>.FailWithMessage(e.Error.Message)),
                    _ => Ok(MyResponse<object>.FailWithMessage(e.Error.Message))
                };
            }
        }
    }
}