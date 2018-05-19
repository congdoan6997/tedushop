using AutoMapper;
using TeduShop.Model.Models;
using TeduShop.Web.Models;

namespace TeduShop.Web.Mappings
{
    public class AutoMapperConfiguration
    {
        public static void Configure()
        {
            //Mapper.Initialize(cfg => {
            //    cfg.CreateMap<Tag, TagViewModel>();
            //    cfg.CreateMap<PostCategory, PostCategoryViewModel>();
            //    cfg.CreateMap<Post, PostViewModel>();
            //    cfg.CreateMap<PostTag, PostTagViewModel>();
            //});

            Mapper.CreateMap<Tag, TagViewModel>();
            Mapper.CreateMap<PostCategory, PostCategoryViewModel>();
            Mapper.CreateMap<Post, PostViewModel>();
            Mapper.CreateMap<PostTag, PostTagViewModel>();
        }
    }
}