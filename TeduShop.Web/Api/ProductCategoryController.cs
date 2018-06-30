using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TeduShop.Model.Models;
using TeduShop.Service;
using TeduShop.Web.Infrastructure.Core;
using TeduShop.Web.Infrastructure.Extensions;
using TeduShop.Web.Models;

namespace TeduShop.Web.Api
{
    [RoutePrefix("api/productcategory")]
    public class ProductCategoryController : ApiControllerBase
    {
        private IProductCategoryService _productCategoryService;
        private IErrorService _errorService;

        public ProductCategoryController(IErrorService errorService, IProductCategoryService productCategoryService) : base(errorService)
        {
            this._errorService = errorService;
            this._productCategoryService = productCategoryService;
        }

        [Route("Getall")]
        [HttpGet]
        public HttpResponseMessage Get(HttpRequestMessage httpRequestMessage, string keyword, int page, int pageSize = 0)
        {
            return CreateHttpResponse(httpRequestMessage, () =>
            {
                int totalRow = 0;
                var categories = this._productCategoryService.GetAll(keyword);
                totalRow = categories.Count();
                var query = categories.OrderByDescending(x => x.CreatedDate).Skip(page * pageSize).Take(pageSize);

                var list = Mapper.Map<List<ProductCategoryViewModel>>(query);

                var pagination = new PaginationSet<ProductCategoryViewModel>()
                {
                    Items = list,
                    Page = page,
                    TotalCount = totalRow,
                    TotalPages = (int)Math.Ceiling((decimal)totalRow / pageSize)
                };
                return httpRequestMessage.CreateResponse(HttpStatusCode.OK, pagination);
            });
        }

        [Route("Getallparents")]
        [HttpGet]
        public HttpResponseMessage Get(HttpRequestMessage httpRequestMessage)
        {
            return CreateHttpResponse(httpRequestMessage, () =>
            {

                var categories = this._productCategoryService.GetAll();

                var list = Mapper.Map<List<ProductCategoryViewModel>>(categories);

                return httpRequestMessage.CreateResponse(HttpStatusCode.OK, list);
            });
        }
        [Route("create")]
        [HttpPost]
        public HttpResponseMessage Create(HttpRequestMessage httpRequestMessage, ProductCategoryViewModel productCategoryViewModel)
        {
            return CreateHttpResponse(httpRequestMessage, () =>
            {
                HttpResponseMessage responseMessage = null;
                if (!ModelState.IsValid)
                {
                   responseMessage = httpRequestMessage.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    var newProductCategory = new ProductCategory();
                    newProductCategory.UpdateProductCategory(productCategoryViewModel);

                    var category = this._productCategoryService.Add(newProductCategory);
                    this._productCategoryService.SaveChanges();

                    responseMessage = httpRequestMessage.CreateResponse(HttpStatusCode.OK, category);
                }

                return responseMessage;
            });
        }

        //public HttpResponseMessage Post(HttpRequestMessage httpRequestMessage, ProductCategoryViewModel productCategoryViewModel)
        //{
        //    return CreateHttpResponse(httpRequestMessage, () =>
        //    {
        //        ProductCategory productCategory = new ProductCategory();
        //        productCategory.u
        //    });
        //}
    }
}