namespace TeduShop.Data.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using TeduShop.Data;
    using TeduShop.Model.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<TeduShopDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(TeduShopDbContext context)
        {
            CreateProductCategorySample(context);
            CreateSlide(context);
            CreatePage(context);
            CreateContactDetail(context);
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
            //var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new TeduShopDbContext()));
            //var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new TeduShopDbContext()));

            //var user = new ApplicationUser()
            //{
            //    UserName = "congdoan",
            //    Email = "congdoan6997@gmail.com",
            //    EmailConfirmed = true,
            //    Birthday = DateTime.Now,
            //    FullName = "Bui Cong Doan",

            //};

            //manager.Create(user, "123654$");
            //if (!roleManager.Roles.Any())
            //{
            //    roleManager.Create(new IdentityRole { Name = "Admin" });
            //    roleManager.Create(new IdentityRole { Name = "User" });
            //}

            //var adminUser = manager.FindByEmail("congdoan6997@gmail.com");

            //manager.AddToRoles(adminUser.Id, new string[] { "Admin", "User" });
        }

        private void CreateProductCategorySample(TeduShopDbContext context)
        {
            if (context.ProductCategories.Count() == 0)
            {
                List<ProductCategory> listProductCategory = new List<ProductCategory>()
            {
                new ProductCategory() { Name="Điện lạnh",Alias="dien-lanh",Status=true },
                 new ProductCategory() { Name="Viễn thông",Alias="vien-thong",Status=true },
                  new ProductCategory() { Name="Đồ gia dụng",Alias="do-gia-dung",Status=true },
                   new ProductCategory() { Name="Mỹ phẩm",Alias="my-pham",Status=true }
            };
                context.ProductCategories.AddRange(listProductCategory);
                context.SaveChanges();
            }
        }

        //private void CreateFooter(TeduShopDbContext context)
        //{
        //    if (context.Footers.Count() == 0)
        //    {
        //        var footer = new Footer()
        //        {
        //             Content = @""
        //        }
        //    }
        //}
        private void CreateSlide(TeduShopDbContext context)
        {
            if (context.Slides.Count() == 0)
            {
                List<Slide> slides = new List<Slide>()
                {
                    new Slide(){ Name = "Slide 1", DisplayOrder = 1, Status = true, URL ="#", Image = "/Assets/client/images/bag.jpg" ,Content = @"<h2>FLAT 50% 0FF</h2>
                                <label>FOR ALL PURCHASE <b>VALUE</b></label>
                                <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et </p>
                                < span class=""on-get"">GET NOW</span>"},
                    new Slide(){ Name = "Slide 2", DisplayOrder = 2, Status = true, URL ="#", Image = "/Assets/client/images/bag1.jpg", Content =@"<h2>FLAT 50% 0FF</h2>
								<label>FOR ALL PURCHASE <b>VALUE</b></label>
								<p>Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et </p>
								<span class=""on-get"">GET NOW</span>"},
                };
                context.Slides.AddRange(slides);
                context.SaveChanges();
            }
        }

        private void CreatePage(TeduShopDbContext context)
        {
            if (context.Pages.Count() == 0)
            {
                var page = new Page()
                {
                    Name = "Giới thiệu",
                    Alias = "gioi-thieu",
                    Content = @"Sed ut perspiciatis unde omnis iste natus error sit voluptatem accusantium doloremque laudantium, totam rem aperiam, eaque ipsa quae ab illo inventore veritatis et quasi architecto beatae vitae dicta sunt explicabo. Nemo enim ipsam voluptatem quia voluptas sit aspernatur aut odit aut fugit, sed quia consequuntur magni dolores eos qui ratione voluptatem sequi nesciunt. Neque porro quisquam est, qui dolorem ipsum quia dolor sit amet, consectetur, adipisci velit, sed quia non numquam eius modi tempora incidunt ut labore et dolore magnam aliquam quaerat voluptatem. Ut enim ad minima veniam, quis nostrum exercitationem ullam corporis suscipit laboriosam, nisi ut aliquid ex ea commodi consequatur? Quis autem vel eum iure reprehenderit qui in ea voluptate velit esse quam nihil molestiae consequatur, vel illum qui dolorem eum fugiat quo voluptas nulla pariatur?",
                    Status = true
                };
                context.Pages.Add(page);
                context.SaveChanges();
            }
        }

        private void CreateContactDetail(TeduShopDbContext context)
        {
            if (context.ContactDetails.Count() == 0)
            {
                var contactdetail = new ContactDetail()
                {
                    Name = "Shop thời trang TEDU",
                    Status = true,
                    Address = "Hưng Thái - Ninh Giang - Hải Dương",
                    Email = "Congdoan6997@gmail.com",
                    Lat = 20.7228056,
                    Lng = 106.294812,
                    Phone = "01695432166",
                    Website = "http://tedu.com.vn"
                };
                context.ContactDetails.Add(contactdetail);
                context.SaveChanges();
            }
        }
    }
}