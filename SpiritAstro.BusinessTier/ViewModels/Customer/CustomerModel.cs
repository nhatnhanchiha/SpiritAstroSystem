using SpiritAstro.BusinessTier.Commons.Attributes;
using System;

namespace SpiritAstro.BusinessTier.ViewModels.Customer
{
    public class CustomerInFollow
    {
        public long Id { get; set; }
        public string Name { get; set; }
    }
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
        public string PlaceOfBirth { get; set; }
        public string NatalChartUrl { get; set; }
    }

    public class PublicCustomerModel
    {
        public static string[] Fields =
            { "Id", "Name", "PhoneNumber", "Gender", "Status", "LatitudeOfBirth", "LongitudeOfBirth", "TimeOfBirth", "PlaceOfBirth" };
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
        public string PlaceOfBirth { get; set; }
        public string NatalChartUrl { get; set; }
    }

    public class PublicCustomerModelForAdmin
    {
        public static string[] Fields =
            { "Id", "Name", "PhoneNumber", "Gender", "Status", "LatitudeOfBirth", "LongitudeOfBirth", "TimeOfBirth", "PlaceOfBirth" };
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
        public string PlaceOfBirth { get; set; }
        public string NatalChartUrl { get; set; }
        [Skip]
        public bool? IsDeleted { get; set; }

    }
}