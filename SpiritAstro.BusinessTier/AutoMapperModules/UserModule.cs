using System.Collections.Generic;
using AutoMapper;
using SpiritAstro.BusinessTier.Commons.Enums.User;
using SpiritAstro.BusinessTier.Requests.User;
using SpiritAstro.BusinessTier.ViewModels.Users;
using SpiritAstro.DataTier.Models;

namespace SpiritAstro.BusinessTier.AutoMapperModules
{
    public static class UserModule
    {
        public static void ConfigUserMapperModule(this IMapperConfigurationExpression mc)
        {
            mc.CreateMap<RegisterCustomerRequest, User>()
                .ForMember(des => des.UserRoles, opt =>
                    opt.MapFrom(ignore => new List<UserRole> { new() { RoleId = "customer" } }))
                .ForMember(des => des.StatusPayment, opt => opt.MapFrom(ignore => (int)UserStatusPayment.Inactive))
                .ForMember(des => des.Status, opt => opt.MapFrom(ignore => (int)UserStatus.Active));
            mc.CreateMap<DetailUserRequest, User>();
            mc.CreateMap<User, UserModels>().ReverseMap();
        }
    }
}