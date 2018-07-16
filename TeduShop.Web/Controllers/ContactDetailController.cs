using AutoMapper;
using BotDetect.Web.Mvc;
using System.Web.Mvc;
using TeduShop.Common;
using TeduShop.Model.Models;
using TeduShop.Service;
using TeduShop.Web.Infrastructure.Extensions;
using TeduShop.Web.Models;

namespace TeduShop.Web.Controllers
{
    public class ContactDetailController : Controller
    {
        private IContactDetailService _contactDetailService;
        private IFeedbackService _feedbackService;

        public ContactDetailController(IContactDetailService contactDetailService, IFeedbackService feedbackService)
        {
            _contactDetailService = contactDetailService;
            this._feedbackService = feedbackService;
        }

        // GET: ContactDetail
        public ActionResult Index()
        {
            FeedbackViewModel feedbackView = new FeedbackViewModel()
            {
                ContactDetail = GetContactDetail()
            };

            return View(feedbackView);
        }

        [HttpPost]
        [AllowAnonymous]
        [CaptchaValidation("CaptchaCode", "ContactCaptcha", "Mã xác nhận không đúng")]
        public ActionResult SendFeedback(FeedbackViewModel feedbackViewModel)
        {
            if (ModelState.IsValid)
            {
                Feedback feedback = new Feedback();
                feedback.UpdateFeedback(feedbackViewModel);
                _feedbackService.Create(feedback);
                _feedbackService.SaveChanges();

                ViewData["SeccessMsg"] = "Gửi phản hổi thành công";

                var content = System.IO.File.ReadAllText(Server.MapPath("/Assets/client/template/contact_template.html"));
                content = content.Replace("{{Name}}", feedback.Name);
                content = content.Replace("{{Email}}", feedback.Email);
                content = content.Replace("{{Message}}", feedback.Message);

                var adminEmail = ConfigHelper.GetByKey("AdminEmail");

                MailHelper.SendMail(adminEmail, "Thông tin liên hệ từ Website", content);
                feedbackViewModel.Name = "";
                feedbackViewModel.Email = "";
                feedbackViewModel.Message = "";
            }
            feedbackViewModel.ContactDetail = GetContactDetail();
            return View("Index", feedbackViewModel);
        }

        private ContactDetailViewModel GetContactDetail()
        {
            var model = _contactDetailService.GetContactDetail();
            return Mapper.Map<ContactDetailViewModel>(model);
        }

        private
    }
}