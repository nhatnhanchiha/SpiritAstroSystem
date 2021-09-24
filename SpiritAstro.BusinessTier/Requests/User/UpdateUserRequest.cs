using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpiritAstro.BusinessTier.Requests.User
{
    public class UpdateUserRequest
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public bool Gender { get; set; }
        public double LatitudeOfBirth { get; set; }
        public double LongitudeOfBirth { get; set; }
        public DateTime TimeOfBirth { get; set; }
    }
}
