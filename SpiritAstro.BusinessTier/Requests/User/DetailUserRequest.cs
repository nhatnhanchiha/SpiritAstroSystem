using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpiritAstro.BusinessTier.Requests.User
{
    class DetailUserRequest
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public int StatusPayment { get; set; }
        public string Password { get; set; }
        public bool Gender { get; set; }
        public int Status { get; set; }
        public double LatitudeOfBirth { get; set; }
        public double LongitudeOfBirth { get; set; }

        public string UrlImage { get; set; }
        public string Email { get; set; }
    }
}
