using AutoMapper;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TeduShop.Model.Models;
using TeduShop.Service;
using TeduShop.Web.Infrastructure.Core;
using TeduShop.Web.Models;
using TeduShop.Web.Infrastructure.Extensions;

namespace TeduShop.Web.Api
{
    [RoutePrefix("api/postcategory")]
    [Authorize]
    public class PostCategoryController : ApiControllerBase
    {
        private IPostCategoryService _postCategoryService;

        public PostCategoryController(IErrorService errorService, IPostCategoryService postCategoryService) : base(errorService)
        {
            this._postCategoryService = postCategoryService;
        }

        [Route("Getall")]
        public HttpResponseMessage Get(HttpRequestMessage httpRequestMessage)
        {
            return CreateHttpResponse(httpRequestMessage, () =>
            {
                var categories = this._postCategoryService.GetAll();

                var listPostCategoryVM = Mapper.Map<List<PostCategoryViewModel>>(categories);

                return httpRequestMessage.CreateResponse(HttpStatusCode.OK, listPostCategoryVM);
            });
        }
        [Route("Add")]
        public HttpResponseMessage Post(HttpRequestMessage httpRequestMessage, PostCategoryViewModel postCategoryViewModel)
        {
            return CreateHttpResponse(httpRequestMessage, () =>
            {
                HttpResponseMessage httpResponseMessage = null;
                if (!ModelState.IsValid)
                {
                    httpResponseMessage = httpRequestMessage.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    PostCategory postCategory = new PostCategory();
                    postCategory.UpdatePostCategory(postCategoryViewModel);

                    var category = this._postCategoryService.Add(postCategory);
                    this._postCategoryService.SaveChanges();
                    httpResponseMessage = httpRequestMessage.CreateResponse(HttpStatusCode.Created, category);
                }
                return httpResponseMessage;
            });
        }
        [Route("Update")]
        public HttpResponseMessage Put(HttpRequestMessage httpRequestMessage, PostCategoryViewModel postCategoryViewModel)
        {
            return CreateHttpResponse(httpRequestMessage, () =>
            {
                HttpResponseMessage httpResponseMessage = null;
                if (!ModelState.IsValid)
                {
                    httpResponseMessage = httpRequestMessage.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {

                    var postCategory = this._postCategoryService.GetById(postCategoryViewModel.ID);
                    postCategory.UpdatePostCategory(postCategoryViewModel);

                    this._postCategoryService.Update(postCategory);
                    this._postCategoryService.SaveChanges();
                    httpResponseMessage = httpRequestMessage.CreateResponse(HttpStatusCode.OK);
                }
                return httpResponseMessage;
            });
        }
        [Route("Delete")]
        public HttpResponseMessage Delete(HttpRequestMessage httpRequestMessage, int id)
        {
            return CreateHttpResponse(httpRequestMessage, () =>
            {
                HttpResponseMessage httpResponseMessage = null;
                if (!ModelState.IsValid)
                {
                    httpRequestMessage.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    this._postCategoryService.Delete(id);
                    this._postCategoryService.SaveChanges();
                    httpResponseMessage = httpRequestMessage.CreateResponse(HttpStatusCode.OK);
                }
                return httpResponseMessage;
            });
        }
    }
}