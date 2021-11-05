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
            mc.CreateMap<CreatePostRequest, Post>()
                .ForMember(des => des.PostZodiacs, opt
                => opt.MapFrom(src => src.ZodiacIds.Select(zi => new PostZodiac {ZodiacId = zi})));
            mc.CreateMap<Post, PostModel>()
                .ForMember(des => des.Zodiacs, opt
                    => opt.MapFrom(src => src.PostZodiacs.Select(pz => new Zodiac
                    {
                        Id = pz.Zodiac.Id,
                        Name = pz.Zodiac.Name
                    })))
                .ForMember(des => des.ZodiacIds, opt => opt.Ignore());
            mc.CreateMap<PostModel, Post>();
            mc.CreateMap<UpdatePostRequest, Post>();
            mc.CreateMap<Post, PostModel>();
        }
    }
}