using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TeduShop.Model.Models;
using TeduShop.Service;
using TeduShop.Web.Infrastructure.Core;

namespace TeduShop.Web.Api
{
    [RoutePrefix("api/postcategory")]
    public class PostCategoryController : ApiControllerBase
    {
        IPostCategoryService _postCategoryService;
        public PostCategoryController(IErrorService errorService,IPostCategoryService postCategoryService) : base(errorService)
        {
            this._postCategoryService = postCategoryService;
        }
        [Route("Getall")]
        public HttpResponseMessage Get(HttpRequestMessage httpRequestMessage)
        {
            return CreateHttpResponse(httpRequestMessage, () =>
            {
                    var categories = this._postCategoryService.GetAll();
                    return httpRequestMessage.CreateResponse(HttpStatusCode.OK, categories);

            });
        }
        public HttpResponseMessage Post(HttpRequestMessage httpRequestMessage, PostCategory postCategory)
        {
            return CreateHttpResponse(httpRequestMessage, () =>
            {
                HttpResponseMessage httpResponseMessage = null;
                if (ModelState.IsValid)
                {
                    httpRequestMessage.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                   var category = this._postCategoryService.Add(postCategory);
                    this._postCategoryService.Save();
                    httpResponseMessage = httpRequestMessage.CreateResponse(HttpStatusCode.Created, category);
                }
                return httpResponseMessage;
            });
        }

        public HttpResponseMessage Put(HttpRequestMessage httpRequestMessage, PostCategory postCategory)
        {
            return CreateHttpResponse(httpRequestMessage, () =>
            {
                HttpResponseMessage httpResponseMessage = null;
                if (ModelState.IsValid)
                {
                    httpRequestMessage.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    this._postCategoryService.Update(postCategory);
                    this._postCategoryService.Save();
                    httpResponseMessage = httpRequestMessage.CreateResponse(HttpStatusCode.OK);
                }
                return httpResponseMessage;
            });
        }
        public HttpResponseMessage Delete(HttpRequestMessage httpRequestMessage, int id)
        {
            return CreateHttpResponse(httpRequestMessage, () =>
            {
                HttpResponseMessage httpResponseMessage = null;
                if (ModelState.IsValid)
                {
                    httpRequestMessage.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                  this._postCategoryService.Delete(id);
                    this._postCategoryService.Save();
                    httpResponseMessage = httpRequestMessage.CreateResponse(HttpStatusCode.OK);
                }
                return httpResponseMessage;
            });
        }



    }
}