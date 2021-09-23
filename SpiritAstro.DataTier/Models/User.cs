using System;
using System.Collections.Generic;

#nullable disable

namespace SpiritAstro.DataTier.Models
{
    public partial class User
    {
        public User()
        {
            BookingAstrologers = new HashSet<Booking>();
            BookingCustomers = new HashSet<Booking>();
            CustomerZodiacs = new HashSet<CustomerZodiac>();
            FieldDetails = new HashSet<FieldDetail>();
            FollowAstrologers = new HashSet<Follow>();
            FollowCustomers = new HashSet<Follow>();
            UserRoles = new HashSet<UserRole>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public int StatusPayment { get; set; }
        public string Password { get; set; }
        public bool Gender { get; set; }
        public int Status { get; set; }
        public double LatitudeOfBirth { get; set; }
        public double LongitudeOfBirth { get; set; }
        public DateTimeOffset? CreatedAt { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
        public DateTimeOffset? DeletedAt { get; set; }
        public DateTime TimeOfBirth { get; set; }

        public virtual ICollection<Booking> BookingAstrologers { get; set; }
        public virtual ICollection<Booking> BookingCustomers { get; set; }
        public virtual ICollection<CustomerZodiac> CustomerZodiacs { get; set; }
        public virtual ICollection<FieldDetail> FieldDetails { get; set; }
        public virtual ICollection<Follow> FollowAstrologers { get; set; }
        public virtual ICollection<Follow> FollowCustomers { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
