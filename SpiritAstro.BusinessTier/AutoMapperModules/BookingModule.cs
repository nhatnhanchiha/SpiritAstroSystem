using AutoMapper;
using SpiritAstro.BusinessTier.Commons.Enums.Booking;
using SpiritAstro.BusinessTier.Requests.Booking;
using SpiritAstro.BusinessTier.Requests.Category;
using SpiritAstro.BusinessTier.ViewModels.Booking;
using SpiritAstro.BusinessTier.ViewModels.Category;
using SpiritAstro.DataTier.Models;

namespace SpiritAstro.BusinessTier.AutoMapperModules
{
    public static class BookingModule
    {
        public static void ConfigBookingMapperModule(this IMapperConfigurationExpression mc)
        {
            mc.CreateMap<Booking, BookingModel>().ReverseMap();
            mc.CreateMap<CreateBookingRequest, Booking>()
                .ForMember(des => des.Status, opt
                    => opt.MapFrom(ignore => (int)BookingStatus.Init));
        }
    }
}