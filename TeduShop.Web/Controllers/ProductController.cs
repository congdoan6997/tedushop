using AutoMapper;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using TeduShop.Common;
using TeduShop.Service;
using TeduShop.Web.Infrastructure.Core;
using TeduShop.Web.Models;

namespace TeduShop.Web.Controllers
{
    public class ProductController : Controller
    {
        private IProductService _productService;
        private IProductCategoryService _productCategoryService;

        public ProductController(IProductService productService, IProductCategoryService productCategoryService)
        {
            this._productService = productService;
            this._productCategoryService = productCategoryService;
        }

        // GET: Product
        public ActionResult Detail(int id)
        {
            var product = this._productService.GetById(id);
            var model = Mapper.Map<ProductViewModel>(product);
            var relatedProduct = this._productService.GetReatedProducts(id, 6);
            ViewBag.RelatedProducts = Mapper.Map<IEnumerable<ProductViewModel>>(relatedProduct);
            List<string> listImages = new JavaScriptSerializer().Deserialize<List<string>>(model.MoreImages);
            ViewBag.MoreImages = listImages;
            ViewBag.Tags = Mapper.Map<IEnumerable<TagViewModel>>(this._productService.GetTagsByProductId(id));
            return View(model);
        }

        public ActionResult Category(int id, int page = 1, string sort = "")
        {
            var pageSize = int.Parse(ConfigHelper.GetByKey("PageSize"));
            int totalRow;
            var list = this._productService.GetListProductByCategoryIdPaging(id, page, pageSize, sort, out totalRow);
            var listViewModel = Mapper.Map<IEnumerable<ProductViewModel>>(list);

            var category = this._productCategoryService.GetById(id);
            ViewBag.Category = Mapper.Map<ProductCategoryViewModel>(category);

            var paginationSet = new PaginationSet<ProductViewModel>()
            {
                Items = listViewModel,
                TotalCount = totalRow,
                Page = page,
                MaxPage = int.Parse(ConfigHelper.GetByKey("MaxPage")),
                TotalPages = (int)Math.Ceiling((double)totalRow / pageSize)
            };

            return View(paginationSet);
        }

        public ActionResult Search(string keyword, int page = 1, string sort = "")
        {
            var pageSize = int.Parse(ConfigHelper.GetByKey("PageSize"));
            int totalRow = 0;
            var list = this._productService.GetListProductByName(keyword, page, pageSize, sort, out totalRow);
            var listViewModel = Mapper.Map<IEnumerable<ProductViewModel>>(list);

            //var category = this._productCategoryService.GetById(id);
            ViewBag.KeyWord = keyword;

            var paginationSet = new PaginationSet<ProductViewModel>()
            {
                Items = listViewModel,
                TotalCount = totalRow,
                Page = page,
                MaxPage = int.Parse(ConfigHelper.GetByKey("MaxPage")),
                TotalPages = (int)Math.Ceiling((double)totalRow / pageSize)
            };

            return View(paginationSet);
        }

        public JsonResult GetListProductByName(string keyword)
        {
            var model = this._productService.GetListProductByName(keyword);
            return Json(new
            {
                data = model
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ListByTag(string tagId, int page = 1)
        {
            var pageSize = int.Parse(ConfigHelper.GetByKey("PageSize"));
            int totalRow = 0;
            var list = this._productService.GetListProductByTag(tagId, page, pageSize, out totalRow);
            var listViewModel = Mapper.Map<IEnumerable<ProductViewModel>>(list);

            //var category = this._productCategoryService.GetById(id);
            ViewBag.Tag = Mapper.Map<TagViewModel>(_productService.GetTag(tagId));

            var paginationSet = new PaginationSet<ProductViewModel>()
            {
                Items = listViewModel,
                TotalCount = totalRow,
                Page = page,
                MaxPage = int.Parse(ConfigHelper.GetByKey("MaxPage")),
                TotalPages = (int)Math.Ceiling((double)totalRow / pageSize)
            };

            return View(paginationSet);
        }
    }
}