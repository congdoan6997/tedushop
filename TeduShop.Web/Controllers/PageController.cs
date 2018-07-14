using AutoMapper;
using System.Web.Mvc;
using TeduShop.Service;
using TeduShop.Web.Models;

namespace TeduShop.Web.Controllers
{
    public class PageController : Controller
    {
        private IPageService _pageService;

        public PageController(IPageService pageService)
        {
            _pageService = pageService;
        }

        // GET: Page
        public ActionResult Index(string alias)
        {
            var page = this._pageService.GetByAlias(alias);
            var pagemodel = Mapper.Map<PageViewModel>(page);
            return View(pagemodel);
        }
    }
}