using AutoMapper;
using System.Collections.Generic;
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
        public HttpResponseMessage Get(HttpRequestMessage httpRequestMessage)
        {
            return CreateHttpResponse(httpRequestMessage, () =>
            {
                var categories = this._productCategoryService.GetAll();
                var list = Mapper.Map<List<ProductCategoryViewModel>>(categories);
                return httpRequestMessage.CreateResponse(HttpStatusCode.OK, list);
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