using AutoMapper;
using SpiritAstro.BusinessTier.Requests.Category;
using SpiritAstro.BusinessTier.Requests.PostZodiac;
using SpiritAstro.BusinessTier.ViewModels.PostZodiac;
using SpiritAstro.DataTier.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpiritAstro.BusinessTier.AutoMapperModules
{
    public static class PostZodiacModule
    {
        public static void ConfigPostZodiacMapperModule(this IMapperConfigurationExpression mc)
        {
            mc.CreateMap<CreatePostZodiacRequest, PostZodiac>();
            mc.CreateMap<PostZodiac, PostZodiacModel>().ReverseMap();
        }
    }
}
