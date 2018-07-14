using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TeduShop.Service;
using TeduShop.Web.Models;

namespace TeduShop.Web.Controllers
{
    public class ContactDetailController : Controller
    {
        IContactDetailService _contactDetailService;

        public ContactDetailController(IContactDetailService contactDetailService)
        {
            _contactDetailService = contactDetailService;
        }
        // GET: ContactDetail
        public ActionResult Index()
        {
            var contact = _contactDetailService.GetContactDetail();

            var viewmodel = Mapper.Map<ContactDetailViewModel>(contact);
            return View(viewmodel);
        }
    }
}