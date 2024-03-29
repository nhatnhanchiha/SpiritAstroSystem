﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpiritAstro.BusinessTier.ViewModels.Users;

namespace SpiritAstro.BusinessTier.ViewModels.UserRole
{
    public class UserRoleModel
    {
        public static string[] Fields =
        {
            "UserId", "RoleId"
        };
       
        public long? UserId { get; set; }
        public string RoleId { get; set; }
        public UserModel User { get; set; } 
    }
}
