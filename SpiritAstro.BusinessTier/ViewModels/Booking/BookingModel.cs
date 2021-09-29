using System;

namespace SpiritAstro.BusinessTier.ViewModels.Booking
{
    public class BookingModel
    {
        public static string[] Fields = { "Id", "AstrologerId", "CustomerId", "StartTime", "EndTime", "Rating", "Note", "Status" };
        public long? Id { get; set; }
        public long? AstrologerId { get; set; }
        public long? CustomerId { get; set; }
        public DateTimeOffset? StartTime { get; set; }
        public DateTimeOffset? EndTime { get; set; }
        public double? Rating { get; set; }
        public string Note { get; set; }
        public int? Status { get; set; }
    }
}