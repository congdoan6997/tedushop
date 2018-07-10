using AutoMapper;
using System.Collections.Generic;
using System.Web.Mvc;
using TeduShop.Service;
using TeduShop.Web.Models;

namespace TeduShop.Web.Controllers
{
    public class HomeController : Controller
    {
        private IProductCategoryService _productCategoryService;
        private ICommonService _commonService;

        public HomeController(IProductCategoryService productCategoryService, ICommonService commonService)
        {
            this._productCategoryService = productCategoryService;
            this._commonService = commonService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [ChildActionOnly]
        public ActionResult Header()
        {
            return PartialView();
        }

        [ChildActionOnly]
        public ActionResult Footer()
        {
            var model = this._commonService.GetFooter();
            var viewmodel = Mapper.Map<FooterViewModel>(model);
            return PartialView(viewmodel);
        }

        [ChildActionOnly]
        public ActionResult Category()
        {
            var model = this._productCategoryService.GetAll();
            var listViewModel = Mapper.Map<IEnumerable<ProductCategoryViewModel>>(model);
            return PartialView(listViewModel);
        }
    }
}