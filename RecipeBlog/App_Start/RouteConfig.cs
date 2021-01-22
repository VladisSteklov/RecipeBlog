using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace RecipeBlog
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute("Contact", "Contact/{*pathinfо}", new { controller = "Home", action = "Contact" });
            routes.MapRoute("About", "About/{*pathinfо}", new { controller = "Home", action = "About" });
            routes.MapRoute("Profile", "Profile/{*pathinfо}", new { controller = "Home", action = "Profile" });
            
            routes.MapRoute("Login", "Login/{*pathinfo}", new { controller = "Account", action = "Login" });
            

            routes.MapRoute("Register", "Register/{*pathinfo}", new { controller = "Account", action = "Register" });
            routes.MapRoute("ResetPassword", "ResetPassword/{*pathinfo}", new { controller = "Account", action = "ResetPassword" });

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{page}",
                defaults: new { controller = "Home", action = "Index", page = UrlParameter.Optional }
            );
        }
    }
}
