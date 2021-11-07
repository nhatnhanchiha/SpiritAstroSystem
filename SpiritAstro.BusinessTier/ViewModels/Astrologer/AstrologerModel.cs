using System;
using SpiritAstro.BusinessTier.Commons.Attributes;

namespace SpiritAstro.BusinessTier.ViewModels.Astrologer
{
    public class AstrologerInFollow
    {
        public long Id { get; set; }
        public string Name { get; set; }
    }
    
    public class AstrologerModel
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
        public string Description { get; set; }
        public string ImageUrl { get; set; }
    }

    public class PublicAstrologerModel
    {
        public static readonly string[] Fields =
        {
            "Id", "Name", "PhoneNumber", "Gender", "Status", "LatitudeOfBirth", "LongitudeOfBirth", "TimeOfBirth",
            "DeletedAt", "Description", "ImageUrl", "FollowersCount"
        };
        public long? Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public bool? Gender { get; set; }
        public int? Status { get; set; }
        public double? LatitudeOfBirth { get; set; }
        public double? LongitudeOfBirth { get; set; }
        public DateTimeOffset? TimeOfBirth { get; set; }
        public DateTimeOffset? DeletedAt { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public string NatalChartUrl { get; set; }
        public int? FollowersCount { get; set; }
    }
    
    public class PublicAstrologerModelForAdmin
    {
        public static readonly string[] Fields =
        {
            "Id", "Name", "PhoneNumber", "Gender", "Status", "LatitudeOfBirth", "LongitudeOfBirth", "TimeOfBirth",
            "DeletedAt", "Description", "ImageUrl", "FollowersCount"
        };
        public long? Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public bool? Gender { get; set; }
        public int? Status { get; set; }
        public double? LatitudeOfBirth { get; set; }
        public double? LongitudeOfBirth { get; set; }
        public DateTimeOffset? TimeOfBirth { get; set; }
        public DateTimeOffset? DeletedAt { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public int? FollowersCount { get; set; }
        public string NatalChartUrl { get; set; }
        [Skip]
        public bool? IsDeleted { get; set; }
    }
}