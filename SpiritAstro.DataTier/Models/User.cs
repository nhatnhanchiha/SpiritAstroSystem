using System;
using System.Collections.Generic;

#nullable disable

namespace SpiritAstro.DataTier.Models
{
    public partial class User
    {
        public User()
        {
            UserRoles = new HashSet<UserRole>();
        }

        public long Id { get; set; }
        public string Uid { get; set; }

        public virtual Astrologer Astrologer { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
