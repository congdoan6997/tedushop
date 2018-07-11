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
        private IProductService _productService;
        private ICommonService _commonService;

        public HomeController(IProductCategoryService productCategoryService,IProductService productService, ICommonService commonService)
        {
            this._productCategoryService = productCategoryService;
            this._commonService = commonService;
            this._productService = productService;
        }

        public ActionResult Index()
        {
            var slides = this._commonService.GetSlides();
            var slideViewModel = Mapper.Map<IEnumerable<SlideViewModel>>(slides);
            var lastest = this._productService.GetLastest(3);
            var hot = this._productService.GetHotProduct(3);
            var lastestViewModel = Mapper.Map<IEnumerable<ProductViewModel>>(lastest);
            var hotViewModel = Mapper.Map<IEnumerable<ProductViewModel>>(hot);
            var homeViewModel = new HomeViewModel()
            {
                Slides = slideViewModel,
                LastesProducts = lastestViewModel,
                HotProducts = hotViewModel
            };
            return View(homeViewModel);
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