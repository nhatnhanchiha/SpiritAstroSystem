using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpiritAstro.BusinessTier.Requests.UserRole
{
    public class CreateUserRoleRequest
    {
        public long UserId { get; set; }
        public string RoleId { get; set; }
    }
}
