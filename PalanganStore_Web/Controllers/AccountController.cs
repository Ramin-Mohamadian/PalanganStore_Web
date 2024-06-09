using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Palangan.Core.Dtos.User;
using Palangan.Core.Generator;
using Palangan.Core.Security;
using Palangan.Core.Services;
using Palangan.Core.Services.Interfaces;
using Palangan.DataLayer.Entities.Users;
using SendEmail;
using System.Collections.Generic;
using System.Security.Claims;
using static Palangan.Core.Dtos.User.AccountViewModels;

namespace PalanganStore_Web.Controllers
{
    public class AccountController : Controller
    {
        private IAccountService _accountService;
        private IViewRenderService _viewRenderService;
        public AccountController(IAccountService accountService, IViewRenderService viewRenderService)
        {
            _accountService=accountService;
            _viewRenderService=viewRenderService;
        }



        public IActionResult Index()
        {
            return View();
        }

        #region Register
        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }



        [HttpPost]
        public IActionResult SignUp(SignUpViewModel signup)
        {
            if (!ModelState.IsValid)
            {
                return View(signup);
            }

            if (_accountService.IsExistUserName(signup.UserName))
            {
                ModelState.AddModelError("UserName", "نام کاربری قبلا وارد شده است");
                return View(signup);
            }

            if (_accountService.IsExistEmail(signup.Email))
            {
                ModelState.AddModelError("Email", "ایمیل وارد شده تکراری میباشد");
                return View(signup);
            }


            int userId = _accountService.RegisterUser(signup);

            var user = _accountService.GetUserById(userId);

            string body = _viewRenderService.RenderToStringAsync("_VerifyEmail", user);

            //ToDo: Send Email

            #region SendEmail

            SendEmail.SendEmail.Send(user.Email, "فعال سازی حساب کاربری", body);
            #endregion

            return PartialView("_SuccessRegisterUser", user);
        }

        #endregion



        #region Activation
        [Route("ActiveUser/{code}")]
        public IActionResult ActiveUser(string code)
        {
            var user = _accountService.GetUserByActiveCode(code);
            ViewBag.Code =_accountService.ActiveAccount(code);
            return View(user);
        }
        #endregion

        #region Login

        [Route("Login")]
        public IActionResult Login()
        {
            return View();
        }



        [HttpPost]
        [Route("Login")]

        public IActionResult Login(LoginViewModel login, string ReturnUrl = "/")
        {

            if (!ModelState.IsValid)
            {
                return View(login);
            }


            var user = _accountService.LoginUser(login);
            if (user==null)
            {
                ModelState.AddModelError("Email", "کاربری با این مشخصات یافت نشد.");

            }
            else
            {
                if (user.IsActive==true)
                {
                    var claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.NameIdentifier,user.UserId.ToString()),
                        new Claim(ClaimTypes.Name, user.UserName)
                     };

                    var Identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(Identity);
                    var propertis = new AuthenticationProperties
                    {
                        IsPersistent=login.RememberMe
                    };

                    HttpContext.SignInAsync(principal, propertis);
                    ViewBag.IsActive = user.IsActive;
                    if (ReturnUrl!="/")
                    {
                        return Redirect(ReturnUrl);
                    }
                    return View("Login");
                }
                else
                {
                    ModelState.AddModelError("Email", "حساب کاربری شما فعال نمی باشد لطفا به ایمیل خود مراجعه کنید");
                }
            }

            return View();
        }
        #endregion

        #region LogOut

        [Route("Logout")]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/Login");
        }
        #endregion


        #region ForgotPassword
        [Route("ForgotPassword")]
        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }


        [Route("ForgotPassword")]
        [HttpPost]
        public IActionResult ForgotPassword(AccountViewModels.ForgotPasswordViewModel forgot)
        {
            if (!ModelState.IsValid)
            {

                return View(forgot);
            }

            User user = _accountService.GetUserByEmail(forgot.Email);
            if (user == null)
            {
                ModelState.AddModelError("Email", "ایمیل وارد شده وجود ندارد");
                return View(forgot);
            }
            var bodyemail = _viewRenderService.RenderToStringAsync("_ForgotPassword", user);
            SendEmail.SendEmail.Send(user.Email, "بازیابی کلمه عبور", bodyemail);
            ViewBag.ForgotPassword = true;
            return View();
        }


        #endregion

        #region RessetPassword


        public IActionResult ResetPasswordc(string id)
        {

            return View(new RessetPasswordViewModel()
            {
                ActiveCode = id
            });
        }



        [HttpPost]
        public IActionResult ResetPasswordc(RessetPasswordViewModel resetPassword)
        {
            if (!ModelState.IsValid)
            {
                return View(resetPassword);
            }

            User user = _accountService.GetUserByActiveCode(resetPassword.ActiveCode);

            if (user == null)
                return NotFound();


            var hashNewPassword = PasswordHelper.EncodePasswordMd5(resetPassword.Password);
            user.Password = hashNewPassword;
            user.ActiveCode=NameGenerator.GenerateUniqeName();
            _accountService.UpdateUser(user);
            ViewBag.Resetpass=true;
            return Redirect("/Login");

            #endregion
        }
    }
}
