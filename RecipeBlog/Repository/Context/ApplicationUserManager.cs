using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.DataProtection;
using RecipeBlog.Infostructure.Services;
using RecipeBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecipeBlog.Repository.Context
{
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser> store)
            : base(store)
        {
        }
        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
        {
            // Создание
            var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(context.Get<ApplicationDbContext>()));

            // Уникальный адрес почты и разрешение имен русскими символами
            manager.UserValidator = new UserValidator<ApplicationUser>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            // Минимальная длина пароля 6 символов
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
            };

            // Подключение Email сервиса для восстановления пароля
            manager.EmailService = new EmailService();

            // Настройка TokenProvider
            var dataProtectionProvider = new DpapiDataProtectionProvider("RecipeBlog");
            var dataProtector = dataProtectionProvider.Create("Email", "Password");
            manager.UserTokenProvider = new DataProtectorTokenProvider<ApplicationUser>(dataProtector);
            return manager;
        }
    }
}