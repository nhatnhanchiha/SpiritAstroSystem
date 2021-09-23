using AutoMapper;
using SpiritAstro.BusinessTier.Requests.Category;
using SpiritAstro.BusinessTier.ViewModels.Category;
using SpiritAstro.DataTier.Models;

namespace SpiritAstro.BusinessTier.AutoMapperModules
{
    public static class CategoryModule
    {
        public static void ConfigCategoryMapperModule(this IMapperConfigurationExpression mc)
        {
            mc.CreateMap<CreateCategoryRequest, Category>();
            mc.CreateMap<Category, CategoryModel>().ReverseMap();
        }
    }
}