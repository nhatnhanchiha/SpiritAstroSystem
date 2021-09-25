using AutoMapper;
using SpiritAstro.BusinessTier.Requests.Post;
using SpiritAstro.BusinessTier.ViewModels.Post;
using SpiritAstro.DataTier.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpiritAstro.BusinessTier.AutoMapperModules
{
    public static class PostModule
    {
        public static void ConfigPostMapperModule(this IMapperConfigurationExpression mc)
        {
            mc.CreateMap<CreatePostRequest, Post>();
            mc.CreateMap<Post, PostModel>().ReverseMap();
            mc.CreateMap<UpdatePostRequest, Post>();
        }
    }
}
