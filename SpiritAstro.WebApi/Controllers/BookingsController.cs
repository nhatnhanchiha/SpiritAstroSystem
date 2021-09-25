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
using SpiritAstro.WebApi.Attributes;

namespace SpiritAstro.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly IBookingService _bookingService;
        private readonly IUserService _userService;

        public BookingsController(IBookingService bookingService, IUserService userService)
        {
            _bookingService = bookingService;
            _userService = userService;
        }

        // Chả biết để làm gì
        [HttpGet("{id:long}")]
        [CasbinAuthorize]
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
        [CasbinAuthorize]
        public async Task<IActionResult> CreateBooking(CreateBookingRequest createBookingRequest)
        {
            var claims = (CustomClaims)HttpContext.Items["claims"];

            try
            {
                await _userService.IsAstrologer(createBookingRequest.AstrologerId);
                var bookingId = await _bookingService.CreateBooking(claims!.UserId, createBookingRequest);
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