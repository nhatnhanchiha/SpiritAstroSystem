using System;
using System.Collections.Generic;

#nullable disable

namespace SpiritAstro.DataTier.Models
{
    public partial class Customer
    {
        public Customer()
        {
            Bookings = new HashSet<Booking>();
            CustomerZodiacs = new HashSet<CustomerZodiac>();
            Follows = new HashSet<Follow>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public int StatusPayment { get; set; }
        public bool Gender { get; set; }
        public double LatitudeOfBirth { get; set; }
        public double LongitudeOfBirth { get; set; }
        public DateTimeOffset TimeOfBirth { get; set; }
        public DateTimeOffset? CreatedAt { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
        public DateTimeOffset? DeletedAt { get; set; }
        public string UrlImage { get; set; }
        public string Email { get; set; }

        public virtual User IdNavigation { get; set; }
        public virtual ICollection<Booking> Bookings { get; set; }
        public virtual ICollection<CustomerZodiac> CustomerZodiacs { get; set; }
        public virtual ICollection<Follow> Follows { get; set; }
    }
}
