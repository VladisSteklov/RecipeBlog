using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using RecipeBlog.Infostructure.Mappers;
using RecipeBlog.Models;
using RecipeBlog.Repository.Context;
using RecipeBlog.ViewModels;

namespace RecipeBlog.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private ApplicationUserManager _userManager;

       
        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
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

        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindAsync(model.Email, model.Password);
                if (user != null & user.EmailConfirmed)
                {
                    // Аутентификация пользователя на основе кукис
                    var claim = await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
                    // Создание и удаление аутентификационных кукис
                    AuthenticationManager.SignOut();
                    // Сохранять кукис после закрытия браузера
                    AuthenticationManager.SignIn(new AuthenticationProperties { IsPersistent = true }, claim);

                    // Перенаправление пользователя
                    if (String.IsNullOrEmpty(returnUrl)) return RedirectToAction("Index", "Home");
                    return Redirect(returnUrl);
                }
                else
                {
                    // Пользователь не существует
                    ModelState.AddModelError("", "Неверный логин или пароль или почтовой адрес не подтвержден при регистрации");
                }
            }
            ViewBag.returnUrl = returnUrl;
            return View(model);
        }

        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var mapper = new RegisterMapper();
                var user = mapper.Map(model);

                // Создание пользователя
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    // токен для подтверждения Email
                    var code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    // ссылка для подтверждения
                    var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code },
                               protocol: Request.Url.Scheme);

                    string bodyMessage = "<h2>Вас приветствует блог \"Рецептик\"</h2><br>" +
                        "<span style=\"font-size:18px; font-family:'Segoe UI', Tahoma, Geneva, Verdana, sans-serif\">" +
                        "Подтвердите вашу учетную запись, щелкнув <a href =\"" + callbackUrl + "\">здесь</a></span>";

                    // отправка письма
                    await UserManager.SendEmailAsync(user.Id, "Подтверждение учетной записи \"Рецептик\"", bodyMessage);
                    // Переход к авторизации
                    return RedirectToAction("Login", "Account");
                }
                AddErrors(result);
            }
            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(string userId, string code)
        {
            // Подтверждение Email
            await UserManager.ConfirmEmailAsync(userId, code);
            return View("Login");
        }

        // GET: /Account/ForgotPassword
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return PartialView("Partial/ForgotPasswordPartial");
        }

        // POST: /Account/ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByEmailAsync(model.Email);
                if (user != null && user.EmailConfirmed)
                {
                    // Генерация индивидуального токена
                    string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                    // Генерация ссылки сброса пароля в письме
                    var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);

                    string bodyMessage = "<h2>Вас приветствует блог \"Рецептик\"</h2><br>" +
                        "<span style=\"font-size:18px; font-family:'Segoe UI', Tahoma, Geneva, Verdana, sans-serif\">" +
                        "Сбросьте ваш пароль, щелкнув <a href =\"" + callbackUrl + "\">здесь</a></span>";

                    // Отправка письма
                    await UserManager.SendEmailAsync(user.Id, "Сброс пароля для кулинарного блога \"Рецептик\"", bodyMessage);
                    ViewBag.ConfirmForgot = "Для сброса пороля пройдите в Ваш почтовой ящик";
                    return View("Login");
                }

                // Пользователь не существует или логин не подтвержден
                ModelState.AddModelError("", "Логин не подтвержден или отсутствует");
            }
            ViewBag.ConfirmForgot = "Логин не подтвержден или отсутствует";
            return View("Login");
        }

        // GET: /Account/ResetPassword
        [AllowAnonymous]
        public ActionResult ResetPassword(string code)
        {
            return View();
        }

        // POST: /Account/ResetPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await UserManager.FindByEmailAsync(model.Email);
            if (user != null)
            {
                var result = await UserManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("Login", "Account");
                }
                AddErrors(result);
                
            }
            ModelState.AddModelError("", "Пользователь с данным логином не существует");
            return View();
        }

        // GET: /Account/LogOff
        [HttpGet]
        public ActionResult LogOff()
        {
            // Удаление кукис и выход из учетной записи
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }
            }

            base.Dispose(disposing);
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }
    }
}