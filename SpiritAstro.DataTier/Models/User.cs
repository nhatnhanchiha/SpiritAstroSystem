using System;
using System.Collections.Generic;

#nullable disable

namespace SpiritAstro.DataTier.Models
{
    public partial class User
    {
        public User()
        {
            InvoiceCustomers = new HashSet<Invoice>();
            InvoiceProfessors = new HashSet<Invoice>();
            UserPosts = new HashSet<UserPost>();
            UserRoles = new HashSet<UserRole>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime DayOfBirth { get; set; }
        public string Password { get; set; }
        public bool Gender { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public virtual ICollection<Invoice> InvoiceCustomers { get; set; }
        public virtual ICollection<Invoice> InvoiceProfessors { get; set; }
        public virtual ICollection<UserPost> UserPosts { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
