using System;
using System.Collections.Generic;

#nullable disable

namespace SpiritAstro.DataTier.Models
{
    public partial class Astrologer
    {
        public Astrologer()
        {
            Bookings = new HashSet<Booking>();
            FieldDetails = new HashSet<FieldDetail>();
            Follows = new HashSet<Follow>();
            Posts = new HashSet<Post>();
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
        public string Description { get; set; }
        public string ImageUrl { get; set; }

        public virtual User IdNavigation { get; set; }
        public virtual ICollection<Booking> Bookings { get; set; }
        public virtual ICollection<FieldDetail> FieldDetails { get; set; }
        public virtual ICollection<Follow> Follows { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
    }
}
