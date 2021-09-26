﻿using AutoMapper;
using SpiritAstro.BusinessTier.ViewModels.Astrologer;
using SpiritAstro.BusinessTier.ViewModels.Customer;
using SpiritAstro.DataTier.Models;

namespace SpiritAstro.BusinessTier.AutoMapperModules
{
    public static class CustomerModule
    {
        public static void ConfigCustomerMapperModule(this IMapperConfigurationExpression mc)
        {
            mc.CreateMap<Customer, CustomerModel>();
            mc.CreateMap<Customer, PublicCustomerModel>();
        }
    }
}