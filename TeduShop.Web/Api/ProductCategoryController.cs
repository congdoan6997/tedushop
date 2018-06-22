using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TeduShop.Service;
using TeduShop.Web.Infrastructure.Core;
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
        public HttpResponseMessage Get(HttpRequestMessage httpRequestMessage, int page, int pageSize = 0)
        {
            return CreateHttpResponse(httpRequestMessage, () =>
            {
                int totalRow = 0;
                var categories = this._productCategoryService.GetAll();
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