using BotDetect.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TeduShop.Common;
using TeduShop.Model.Models;
using TeduShop.Web.App_Start;
using TeduShop.Web.Models;

namespace TeduShop.Web.Controllers
{
    public class AccountController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public AccountController()
        {
        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        // GET: Account
        public ActionResult Login()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [CaptchaValidation("CaptchaCode", "registerCaptcha", "Mã xác nhận không đúng")]
        public async Task<ActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {
                if ((_userManager.FindByEmailAsync(registerViewModel.Email)).Result != null)
                {
                    ModelState.AddModelError("email", "Email đã tồn tại");
                    return View(registerViewModel);
                }
                if ((_userManager.FindByNameAsync(registerViewModel.UserName)).Result != null)
                {
                    ModelState.AddModelError("username", "UserName đã tồn tại");
                    return View(registerViewModel);
                }

                var user = new ApplicationUser()
                {
                    UserName = registerViewModel.UserName,
                    Email = registerViewModel.Email,
                    EmailConfirmed = true,
                    Birthday = DateTime.Now,
                    FullName = registerViewModel.FullName,
                    PhoneNumber = registerViewModel.PhoneNumber,
                    Address = registerViewModel.Address
                };

                await _userManager.CreateAsync(user, registerViewModel.Password);

                var adminUser = await _userManager.FindByEmailAsync(registerViewModel.Email);
                if (adminUser != null)
                {
                    await _userManager.AddToRoleAsync(adminUser.Id, "User");
                }

                string content = System.IO.File.ReadAllText(Server.MapPath("/Assets/client/template/newuser.html"));
                content = content.Replace("{{Name}}", registerViewModel.FullName);
                content = content.Replace("{{Email}}", registerViewModel.Email);
                content = content.Replace("{{Link}}", ConfigHelper.GetByKey("link"));

                MailHelper.SendMail(registerViewModel.Email, "Đăng ký thành công", content);
                TempData["SuccessMsg"] = "Đăng ký thành công";
            }

            return View();
        }
    }
}