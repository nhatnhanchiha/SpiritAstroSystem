using AutoMapper;
using SpiritAstro.BusinessTier.Requests.UserRole;
using SpiritAstro.BusinessTier.ViewModels.UserRole;
using SpiritAstro.DataTier.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpiritAstro.BusinessTier.AutoMapperModules
{
    public static class UserRoleMudle
    {
        public static void ConfigUserRoleMapperModule(this IMapperConfigurationExpression mc)
        {
            mc.CreateMap<CreateUserRoleRequest, UserRole>();
            mc.CreateMap<UserRole, UserRoleModel>()
                .ForMember(des => des.User, opt => opt.MapFrom(
                    src => src.User));
            mc.CreateMap<UserRoleModel, UserRole>();
            mc.CreateMap<UpdateUserRoleRequest, UserRole>();
        }
    }
}
