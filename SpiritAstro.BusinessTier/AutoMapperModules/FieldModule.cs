using AutoMapper;
using SpiritAstro.BusinessTier.Requests.Field;
using SpiritAstro.BusinessTier.ViewModels.Field;
using SpiritAstro.DataTier.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpiritAstro.BusinessTier.AutoMapperModules
{
    public static class FieldModule
    {
        public static void ConfigFieldMapperModule(this IMapperConfigurationExpression mc)
        {
            mc.CreateMap<CreateFieldRequest, Field>();
            mc.CreateMap<Field, FieldModel>().ReverseMap();
            //mc.CreateMap<UpdateCategoryRequest, Category>();
        }
    }
}
