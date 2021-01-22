using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using PagedList;
using RecipeBlog.Infostructure.Mappers;
using RecipeBlog.Infostructure.Services;
using RecipeBlog.Models;
using RecipeBlog.Repository;
using RecipeBlog.ViewModels;

namespace RecipeBlog.Controllers
{
    public class HomeController : Controller
    {
        private const int _countRecipesInPage = 6;
        private RecipeRepository _recipeRepository;
        private FilterCookiesSerivce _cookiesService;

        public HomeController()
        {
            _recipeRepository = new RecipeRepository();
        }
        public HomeController(RecipeRepository repository)
        {
            _recipeRepository = repository;
        }

        [HttpGet]
        public async Task<ActionResult> Index(int? page)
        {
            _cookiesService = new FilterCookiesSerivce(FilterRecipeTypes.FILTER_TYPE.None);
            _cookiesService.SetCookies(this.HttpContext);

            var mapper = new RecipeIndexRecipeMapper();
            var recipes = mapper.Map(await _recipeRepository.GetRecipesAsync());

            ViewBag.RecipePage = recipes.ToPagedList(page ?? 1, _countRecipesInPage);
            return View();
        }
        [HttpGet]
        public async Task<ActionResult> Popular(int? page)
        {
            _cookiesService = new FilterCookiesSerivce(FilterRecipeTypes.FILTER_TYPE.Populars);
            _cookiesService.SetCookies(this.HttpContext);

            var mapper = new RecipeIndexRecipeMapper();
            var recipes = mapper.Map(await _recipeRepository.GetPopularRecipesAsync());

            ViewBag.RecipePage = recipes.ToPagedList(page ?? 1, _countRecipesInPage);
            return View("Index");

        }
        [HttpGet]
        public async Task<ActionResult> New(int? page)
        {
            _cookiesService = new FilterCookiesSerivce(FilterRecipeTypes.FILTER_TYPE.News);
            _cookiesService.SetCookies(this.HttpContext);

            var mapper = new RecipeIndexRecipeMapper();
            var recipes = mapper.Map(await _recipeRepository.GetNewRecipesAsync());

            ViewBag.RecipePage = recipes.ToPagedList(page ?? 1, _countRecipesInPage);
            return View("Index");
        }

        [HttpGet]
        public async Task<ActionResult> Filter(int? page)
        {
            _cookiesService = new FilterCookiesSerivce();

            var cookies = _cookiesService.GetCookiesFilter(this.HttpContext);

            var mapper = new RecipeIndexRecipeMapper();
            var recipes = mapper.Map(await _recipeRepository.GetRecipeWithFilterAsync(
                cookies.Meal, cookies.CountryKitchen,
                FilterCookiesSerivce.CheckFilterBool(cookies.PhotoAvailability),
                FilterCookiesSerivce.CheckFilterBool(cookies.CommentsAvailability)));

            _cookiesService.SetCookies(HttpContext);

            ViewBag.RecipePage = recipes.ToPagedList(page ?? 1, _countRecipesInPage);
            return View("Index");
        }

        [HttpPost]
        public ActionResult Filter(FilterRecipeViewModel filter)
        {
            if (ModelState.IsValid)
            {
                _cookiesService = new FilterCookiesSerivce(filter, FilterRecipeTypes.FILTER_TYPE.Filters);
                _cookiesService.SetCookies(this.HttpContext);

                return RedirectToAction("Filter");
            }

            return View("Index");
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }
        
        [Authorize]
        public new ActionResult Profile()
        {
            // Перенаправление на страницу пользователя
            return RedirectToAction("User", "User", new { userId = HttpContext.User.Identity.GetUserId() });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_recipeRepository != null)
                {
                    _recipeRepository.Dispose();
                    _recipeRepository = null;
                }
            }
            base.Dispose(disposing);
        }

    }
}