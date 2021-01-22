using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using RecipeBlog.Repository.Context;

[assembly: OwinStartupAttribute(typeof(RecipeBlog.Startup))]
namespace RecipeBlog
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // Настройка контекста базы данных и менеджера пользователей
            app.CreatePerOwinContext(ApplicationDbContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);

            // Установка аутентификационных кукис
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                // Перенаправление неавторизованных пользователей
                LoginPath = new PathString("/Account/Login"),
            });
        }
    }
}
