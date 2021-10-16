using System;

namespace SpiritAstro.BusinessTier.Requests.Astrologer
{
    public class UpdateAstrologerRequest
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public bool Gender { get; set; }
        public double LatitudeOfBirth { get; set; }
        public double LongitudeOfBirth { get; set; }
        public DateTimeOffset TimeOfBirth { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
    }
}