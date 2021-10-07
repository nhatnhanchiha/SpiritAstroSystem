using System;

namespace SpiritAstro.BusinessTier.ViewModels.Customer
{
    public class CustomerModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public int StatusPayment { get; set; }
        public bool Gender { get; set; }
        public int Status { get; set; }
        public double LatitudeOfBirth { get; set; }
        public double LongitudeOfBirth { get; set; }
        public DateTimeOffset TimeOfBirth { get; set; }
        public DateTimeOffset? CreatedAt { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
        public DateTimeOffset? DeletedAt { get; set; }
        public string UrlImage { get; set; }
        public string Email { get; set; }
    }

    public class PublicCustomerModel
    {
        public static string[] Fields =
            { "Id", "Name", "PhoneNumber", "Gender", "Status", "LatitudeOfBirth", "LongitudeOfBirth", "TimeOfBirth" };
        public long? Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public bool? Gender { get; set; }
        public int? Status { get; set; }
        public double? LatitudeOfBirth { get; set; }
        public double? LongitudeOfBirth { get; set; }
        public DateTimeOffset? TimeOfBirth { get; set; }
        public DateTimeOffset? DeletedAt { get; set; }
        public string UrlImage { get; set; }
    }
}