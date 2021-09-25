using System;

namespace SpiritAstro.BusinessTier.Requests.Booking
{
    public class CreateBookingRequest
    {
        public long AstrologerId { get; set; }
        public DateTimeOffset StartTime { get; set; }
        public DateTimeOffset EndTime { get; set; }
        public string Note { get; set; }
    }
}