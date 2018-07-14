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

#pragma warning disable CS0618 // Type or member is obsolete
            Mapper.CreateMap<Tag, TagViewModel>();
            Mapper.CreateMap<PostCategory, PostCategoryViewModel>();
            Mapper.CreateMap<Post, PostViewModel>();
            Mapper.CreateMap<PostTag, PostTagViewModel>();
            Mapper.CreateMap<Product, ProductViewModel>();
            Mapper.CreateMap<ProductCategory, ProductCategoryViewModel>();
            Mapper.CreateMap<ProductTag, ProductTagViewModel>();
            Mapper.CreateMap<Order, OrderViewModel>();
            Mapper.CreateMap<OrderDetail, OrderDetailViewModel>();
            Mapper.CreateMap<Footer, FooterViewModel>();
            Mapper.CreateMap<Slide, SlideViewModel>();
            Mapper.CreateMap<Page, PageViewModel>();
            Mapper.CreateMap<ContactDetail, ContactDetailViewModel>();
#pragma warning restore CS0618 // Type or member is obsolete
        }
    }
}