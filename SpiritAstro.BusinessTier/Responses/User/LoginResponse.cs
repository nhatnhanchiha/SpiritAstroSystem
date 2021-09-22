using System;

namespace SpiritAstro.BusinessTier.Responses.User
{
    public class LoginResponse
    {
        public long UserId { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime TimeOfBirth { get; set; }
        public bool Gender { get; set; }
        public string Roles { get; set; }
        public string Token { get; set; }
        public long ExpiresAt { get; set; }
        public long BufferTime { get; set; }
    }
}