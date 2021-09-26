﻿using System;
using SpiritAstro.BusinessTier.Commons.Attributes;

namespace SpiritAstro.BusinessTier.ViewModels.Users
{
    public class UserModel
    {
        public long? Id { get; set; }
        [String]
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public int? StatusPayment { get; set; }
        public bool? Gender { get; set; }
        public int? Status { get; set; }
        public double? LatitudeOfBirth { get; set; }
        public double? LongitudeOfBirth { get; set; }
        public DateTime? TimeOfBirth { get; set; }
    }

    public class PublicUserModel
    {
        public long? Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public bool? Gender { get; set; }
        public double? LatitudeOfBirth { get; set; }
        public double? LongitudeOfBirth { get; set; }
        public DateTime TimeOfBirth { get; set; }
        public DateTimeOffset? DeletedAt { get; set; }
    }
}
