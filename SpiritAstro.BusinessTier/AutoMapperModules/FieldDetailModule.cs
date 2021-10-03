using AutoMapper;
using SpiritAstro.BusinessTier.Requests.Astrologer;
using SpiritAstro.BusinessTier.Requests.FieldDetail;
using SpiritAstro.BusinessTier.ViewModels.Astrologer;
using SpiritAstro.BusinessTier.ViewModels.FieldDetail;
using SpiritAstro.DataTier.Models;

namespace SpiritAstro.BusinessTier.AutoMapperModules
{
    public static class FieldDetailModule
    {
        public static void ConfigFieldDetailMapperModule(this IMapperConfigurationExpression mc)
        {
            mc.CreateMap<FieldDetail, FieldDetailModel>();
            mc.CreateMap<CreateFieldDetailRequest, FieldDetail>();
        }
    }
}