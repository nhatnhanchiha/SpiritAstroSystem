using System;
using System.Collections.Generic;

#nullable disable

namespace SpiritAstro.DataTier.Models
{
    public partial class Invoice
    {
        public int Id { get; set; }
        public int ProfessorId { get; set; }
        public int CustomerId { get; set; }
        public double Price { get; set; }
        public int Time { get; set; }
        public DateTime? Date { get; set; }
        public DateTime? DeletedAt { get; set; }

        public virtual User Customer { get; set; }
        public virtual User Professor { get; set; }
    }
}
