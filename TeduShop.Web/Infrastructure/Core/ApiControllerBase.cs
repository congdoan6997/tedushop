using System;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TeduShop.Model.Models;
using TeduShop.Service;

namespace TeduShop.Web.Infrastructure.Core
{
    public class ApiControllerBase : ApiController
    {
        private IErrorService _errorService;

        public ApiControllerBase(IErrorService errorService)
        {
            this._errorService = errorService;
        }

        protected HttpResponseMessage CreateHttpResponse(HttpRequestMessage httpRequestMessage, Func<HttpResponseMessage> func)
        {
            HttpResponseMessage httpResponseMessage = null;
            try
            {
                httpResponseMessage = func.Invoke();
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Trace.WriteLine($"Entity of type \"{eve.Entry.Entity.GetType().Name}\" in state \"{ eve.Entry.State}\" has the following validation errors:");
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Trace.WriteLine($"- Property: \"{ve.PropertyName}\", Error: \"{ve.ErrorMessage}\"");
                    }
                }
                LogError(e);
                httpResponseMessage = httpRequestMessage.CreateResponse(HttpStatusCode.BadRequest, e.InnerException.Message);
            }
            catch (DbUpdateException dbEx)
            {
                LogError(dbEx);
                httpResponseMessage = httpRequestMessage.CreateResponse(HttpStatusCode.BadRequest, dbEx.InnerException.Message);
            }
            catch (Exception ex)
            {
                LogError(ex);
                httpResponseMessage = httpRequestMessage.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
            return httpResponseMessage;
        }

        private void LogError(Exception exception)
        {
            try
            {
                Error error = new Error()
                {
                    CreateDate = DateTime.Now,
                    Message = exception.Message,
                    StackTrace = exception.StackTrace
                };
                this._errorService.Create(error);
                this._errorService.Save();
            }
            catch
            {
               throw;
            }
        }
    }
}