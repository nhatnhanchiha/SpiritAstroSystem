using System;

namespace SpiritAstro.BusinessTier.Requests.Astrologer
{
    public class RegisterAstrologerRequest
    {
        public long UserId { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public int StatusPayment { get; set; }
        public bool Gender { get; set; }
        public double LatitudeOfBirth { get; set; }
        public double LongitudeOfBirth { get; set; }
        public DateTimeOffset TimeOfBirth { get; set; }
    }
}