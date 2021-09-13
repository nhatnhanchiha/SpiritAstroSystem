using System;
using System.Collections.Generic;

#nullable disable

namespace SpiritAstro.DataTier.Models
{
    public partial class UserPost
    {
        public int UserId { get; set; }
        public int PostId { get; set; }

        public virtual Post Post { get; set; }
        public virtual User User { get; set; }
    }
}
