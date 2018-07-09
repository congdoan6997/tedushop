using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Script.Serialization;
using TeduShop.Model.Models;
using TeduShop.Service;
using TeduShop.Web.Infrastructure.Core;
using TeduShop.Web.Infrastructure.Extensions;
using TeduShop.Web.Models;

namespace TeduShop.Web.Api
{
    [RoutePrefix("api/product")]
    [Authorize]
    public class ProductController : ApiControllerBase
    {
        private IProductService _productService;
        private IErrorService _errorService;

        public ProductController(IErrorService errorService, IProductService productService) : base(errorService)
        {
            this._errorService = errorService;
            this._productService = productService;
        }

        [Route("Getall")]
        [HttpGet]
        public HttpResponseMessage Get(HttpRequestMessage httpRequestMessage, string keyword, int page, int pageSize = 0)
        {
            return CreateHttpResponse(httpRequestMessage, () =>
            {
                int totalRow = 0;
                var products = this._productService.GetAll(keyword);
                totalRow = products.Count();
                var query = products.OrderByDescending(x => x.CreatedDate).Skip(page * pageSize).Take(pageSize);

                var list = Mapper.Map<List<ProductViewModel>>(query);

                var pagination = new PaginationSet<ProductViewModel>()
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
                var products = this._productService.GetAll();

                var list = Mapper.Map<List<ProductViewModel>>(products);

                return httpRequestMessage.CreateResponse(HttpStatusCode.OK, list);
            });
        }

        [Route("getbyid/{id:int}")]
        [HttpGet]
        public HttpResponseMessage GetById(HttpRequestMessage httpRequestMessage, int id)
        {
            return CreateHttpResponse(httpRequestMessage, () =>
            {
                var items = this._productService.GetById(id);

                var itemsViewModel = Mapper.Map<ProductViewModel>(items);

                return httpRequestMessage.CreateResponse(HttpStatusCode.OK, itemsViewModel);
            });
        }

        [Route("create")]
        [HttpPost]
        public HttpResponseMessage Create(HttpRequestMessage httpRequestMessage, ProductViewModel productViewModel)
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
                    var newProduct = new Product();
                    newProduct.UpdateProduct(productViewModel);
                    newProduct.CreatedDate = DateTime.Now;
                    newProduct.CreatedBy = User.Identity.Name;
                    var product = this._productService.Add(newProduct);
                    this._productService.SaveChanges();

                    responseMessage = httpRequestMessage.CreateResponse(HttpStatusCode.OK, product);
                }

                return responseMessage;
            });
        }

        [Route("update")]
        [HttpPut]
        public HttpResponseMessage Update(HttpRequestMessage httpRequestMessage, ProductViewModel productViewModel)
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
                    var updateProduct = this._productService.GetById(productViewModel.ID);
                    updateProduct.UpdateProduct(productViewModel);
                    updateProduct.UpdatedDate = DateTime.Now;
                    updateProduct.UpdatedBy = User.Identity.Name;
                    this._productService.Update(updateProduct);
                    this._productService.SaveChanges();

                    responseMessage = httpRequestMessage.CreateResponse(HttpStatusCode.OK, updateProduct);
                }

                return responseMessage;
            });
        }

        [Route("delete")]
        [HttpDelete]
        [AllowAnonymous]
        public HttpResponseMessage Delete(HttpRequestMessage httpRequestMessage, int id)
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
                    //var updateProduct = this._productService.GetById(id);
                    //updateProduct.UpdateProduct(productViewModel);
                    //updateProduct.CreatedDate = DateTime.Now;
                    var item = this._productService.Delete(id);
                    this._productService.SaveChanges();

                    responseMessage = httpRequestMessage.CreateResponse(HttpStatusCode.OK, item);
                }

                return responseMessage;
            });
        }

        [Route("deletemulti")]
        [HttpDelete]
        [AllowAnonymous]
        public HttpResponseMessage Delete(HttpRequestMessage httpRequestMessage, string listId)
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
                    //var updateProduct = this._productService.GetById(id);
                    //updateProduct.UpdateProduct(productViewModel);
                    //updateProduct.CreatedDate = DateTime.Now;
                    var items = new JavaScriptSerializer().Deserialize<List<int>>(listId);
                    foreach (var item in items)
                    {
                        this._productService.Delete(item);
                    }
                    this._productService.SaveChanges();

                    responseMessage = httpRequestMessage.CreateResponse(HttpStatusCode.OK, true);
                }

                return responseMessage;
            });
        }
    }
}