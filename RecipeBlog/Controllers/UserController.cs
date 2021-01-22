using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using PagedList;
using RecipeBlog.Infostructure.Mappers;
using RecipeBlog.Infostructure.Services;
using RecipeBlog.Models;
using RecipeBlog.Repository.Context;
using RecipeBlog.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace RecipeBlog.Controllers
{
    public class UserController : Controller
    {
        private ApplicationUserManager _userManager;

        private const int _countRecipesInPage = 3;

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

        // GET: User
        public new async Task<ActionResult> User(string userId, int? page)
        {
            var user = await UserManager.FindByIdAsync(userId);

            var userMapper = new DefaultMapper<ApplicationUser, UserInfoViewModel>();
            var recipesMapper = new RecipeMapper();

            var userViewModel = userMapper.Map(user);
            userViewModel.CanCreateRecipe = (HttpContext.User.Identity.IsAuthenticated &
                HttpContext.User.Identity.GetUserId() == userId) ?
                true : false;

            var recipesViewModels = recipesMapper.Map(user.Recipes.OrderByDescending(recipe => recipe.CreationTime).ToList());
            recipesViewModels.ForEach(recipe =>
            {
                recipe.IsDeleted = (HttpContext.User.Identity.IsAuthenticated &
                    recipe.AuthorId == HttpContext.User.Identity.GetUserId()) ? true : false;
                recipe.Tag = recipe.Tag.Replace("Не важно", "");
            });

            ViewBag.UserInfo = userViewModel;
            ViewBag.RecipePage = recipesViewModels.ToPagedList(page ?? 1, _countRecipesInPage);
            ViewBag.UserId = userId;

            return View();
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
    }
}