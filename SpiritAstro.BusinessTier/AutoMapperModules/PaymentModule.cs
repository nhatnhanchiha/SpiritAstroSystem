using AutoMapper;
using SpiritAstro.BusinessTier.Requests.Payment;
using SpiritAstro.BusinessTier.ViewModels.Payment;
using SpiritAstro.DataTier.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpiritAstro.BusinessTier.AutoMapperModules
{
    public static class PaymentModule
    {
        public static void ConfigPaymentMapperModule(this IMapperConfigurationExpression mc)
        {
            mc.CreateMap<CreatePaymentRequest, Payment>();
            mc.CreateMap<Payment, PaymentModel>().ReverseMap();
            mc.CreateMap<UpdatePaymentRequest, Payment>();
        }
    }
}
