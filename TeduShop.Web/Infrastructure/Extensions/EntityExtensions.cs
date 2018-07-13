using TeduShop.Model.Models;
using TeduShop.Web.Models;

namespace TeduShop.Web.Infrastructure.Extensions
{
    public static class EntityExtensions
    {
        public static void UpdatePostCategory(this PostCategory postCategory, PostCategoryViewModel postCategoryViewModel)
        {
            #region properties

            //      public int ID { get; set; }

            //public string Name { get; set; }

            //public string Alias { get; set; }

            //public string Description { get; set; }
            //public int? ParentID { get; set; }
            //public int? DisplayOrder { get; set; }

            //public string Image { get; set; }
            //public bool? HomFlag { get; set; }

            //public virtual IEnumerable<PostViewModel> Posts { get; set; }
            //       public DateTime? CreatedDate { get; set; }

            //[MaxLength(256)]
            //public string CreatedBy { get; set; }

            //public DateTime? UpdatedDate { get; set; }

            //[MaxLength(256)]
            //public string UpdatedBy { get; set; }

            //[MaxLength(256)]
            //public string MetaKeyword { get; set; }

            //[MaxLength(256)]
            //public string MetaDescription { get; set; }

            //public bool Status { get; set; }
            //       public DateTime? CreatedDate { get; set; }

            //[MaxLength(256)]
            //public string CreatedBy { get; set; }

            //public DateTime? UpdatedDate { get; set; }

            //[MaxLength(256)]
            //public string UpdatedBy { get; set; }

            //[MaxLength(256)]
            //public string MetaKeyword { get; set; }

            //[MaxLength(256)]
            //public string MetaDescription { get; set; }

            //public bool Status { get; set; }
# endregion
            postCategory.ID = postCategoryViewModel.ID;
            postCategory.Name = postCategoryViewModel.Name;
            postCategory.Alias = postCategoryViewModel.Alias;
            postCategory.Description = postCategoryViewModel.Description;
            postCategory.ParentID = postCategoryViewModel.ParentID;
            postCategory.DisplayOrder = postCategoryViewModel.DisplayOrder;
            postCategory.Image = postCategoryViewModel.Image;
            postCategory.HomFlag = postCategoryViewModel.HomFlag;

            postCategory.CreatedDate = postCategoryViewModel.CreatedDate;
            postCategory.CreatedBy = postCategoryViewModel.CreatedBy;
            postCategory.UpdatedDate = postCategoryViewModel.UpdatedDate;
            postCategory.UpdatedBy = postCategoryViewModel.UpdatedBy;
            postCategory.MetaKeyword = postCategoryViewModel.MetaKeyword;
            postCategory.MetaDescription = postCategoryViewModel.MetaDescription;
            postCategory.Status = postCategoryViewModel.Status;
        }

        public static void UpdateProductCategory(this ProductCategory productCategory, ProductCategoryViewModel productCategoryViewModel)
        {
            #region properties

            //      public int ID { get; set; }

            //public string Name { get; set; }

            //public string Alias { get; set; }

            //public string Description { get; set; }
            //public int? ParentID { get; set; }
            //public int? DisplayOrder { get; set; }

            //public string Image { get; set; }
            //public bool? HomFlag { get; set; }

            //public virtual IEnumerable<PostViewModel> Posts { get; set; }
            //       public DateTime? CreatedDate { get; set; }

            //[MaxLength(256)]
            //public string CreatedBy { get; set; }

            //public DateTime? UpdatedDate { get; set; }

            //[MaxLength(256)]
            //public string UpdatedBy { get; set; }

            //[MaxLength(256)]
            //public string MetaKeyword { get; set; }

            //[MaxLength(256)]
            //public string MetaDescription { get; set; }

            //public bool Status { get; set; }
            //       public DateTime? CreatedDate { get; set; }

            //[MaxLength(256)]
            //public string CreatedBy { get; set; }

            //public DateTime? UpdatedDate { get; set; }

            //[MaxLength(256)]
            //public string UpdatedBy { get; set; }

            //[MaxLength(256)]
            //public string MetaKeyword { get; set; }

            //[MaxLength(256)]
            //public string MetaDescription { get; set; }

            //public bool Status { get; set; }
            #endregion
            productCategory.ID = productCategoryViewModel.ID;
            productCategory.Name = productCategoryViewModel.Name;
            productCategory.Alias = productCategoryViewModel.Alias;
            productCategory.Description = productCategoryViewModel.Description;
            productCategory.ParentID = productCategoryViewModel.ParentID;
            productCategory.DisplayOrder = productCategoryViewModel.DisplayOrder;
            productCategory.Image = productCategoryViewModel.Image;
            productCategory.HomFlag = productCategoryViewModel.HomFlag;

            productCategory.CreatedDate = productCategoryViewModel.CreatedDate;
            productCategory.CreatedBy = productCategoryViewModel.CreatedBy;
            productCategory.UpdatedDate = productCategoryViewModel.UpdatedDate;
            productCategory.UpdatedBy = productCategoryViewModel.UpdatedBy;
            productCategory.MetaKeyword = productCategoryViewModel.MetaKeyword;
            productCategory.MetaDescription = productCategoryViewModel.MetaDescription;
            productCategory.Status = productCategoryViewModel.Status;
        }   

        public static void UpdatePost(this Post post, PostViewModel postViewModel)
        {
            //     public int ID { get; set; }

            //public string Name { get; set; }

            //public string Alias { get; set; }

            //public int CategoryID { get; set; }

            //public string Image { get; set; }

            //public string Description { get; set; }
            //public string Content { get; set; }

            //public bool? HomeFlag { get; set; }
            //public bool? HotFlag { get; set; }
            //public int? ViewCount { get; set; }
            post.ID = postViewModel.ID;
            post.Name = postViewModel.Name;
            post.Alias = postViewModel.Alias;
            post.Description = postViewModel.Description;
            post.CategoryID = postViewModel.CategoryID;
            post.HotFlag = postViewModel.HotFlag;
            post.Image = postViewModel.Image;
            post.Content = postViewModel.Content;
            post.ViewCount = postViewModel.ViewCount;
            post.HomeFlag = postViewModel.HomeFlag;

            post.CreatedBy = postViewModel.CreatedBy;
            post.UpdatedDate = postViewModel.UpdatedDate;
            post.UpdatedBy = postViewModel.UpdatedBy;
            post.MetaKeyword = postViewModel.MetaKeyword;
            post.MetaDescription = postViewModel.MetaDescription;
            post.Status = postViewModel.Status;
        }

        public static void UpdateProduct(this Product product, ProductViewModel productViewModel)
        {
           
            product.ID = productViewModel.ID;
            product.Name = productViewModel.Name;
            product.Alias = productViewModel.Alias;
            product.CategoryID = productViewModel.CategoryID;
            product.Description = productViewModel.Description;
            //product.ParentID = productCategoryViewModel.ParentID;
            //product.DisplayOrder = productCategoryViewModel.DisplayOrder;
            product.Image = productViewModel.Image;
            product.MoreImages = productViewModel.MoreImages;
            product.Price = productViewModel.Price;
            product.PromotionPrice = productViewModel.PromotionPrice;
            product.Warranty = productViewModel.Warranty;
            product.Content = productViewModel.Content;
            product.HomeFlag = productViewModel.HomeFlag;
            product.HotFlag = productViewModel.HotFlag;
            product.ViewCount = productViewModel.ViewCount;

            product.CreatedDate = productViewModel.CreatedDate;
            product.CreatedBy = productViewModel.CreatedBy;
            product.UpdatedDate = productViewModel.UpdatedDate;
            product.UpdatedBy = productViewModel.UpdatedBy;
            product.MetaKeyword = productViewModel.MetaKeyword;
            product.MetaDescription = productViewModel.MetaDescription;
            product.Status = productViewModel.Status;
            product.Tags = productViewModel.Tags;

            product.Quantity = productViewModel.Quantity;

            //product.ProductCategory = productViewModel.ProductCategory;
        }
    }
}