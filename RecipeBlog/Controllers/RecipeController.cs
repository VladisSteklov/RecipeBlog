using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using RecipeBlog.Infostructure.Mappers;
using RecipeBlog.Infostructure.Services;
using RecipeBlog.Models;
using RecipeBlog.Repository;
using RecipeBlog.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace RecipeBlog.Controllers
{
    public class RecipeController : Controller
    {
        RecipeRepository _recipeRepository;

        private string _userImageDirectory = @"C:\Users\Admin\source\repos\RecipeBlog\RecipeBlog\Content\UserImages\";
        public RecipeController()
        {
            _recipeRepository = new RecipeRepository();
        }
        public RecipeController(RecipeRepository repository)
        {
            _recipeRepository = repository;
        }

        // GET: Recipe
        [HttpGet]
        public async Task<ActionResult> Recipe(int recipeId)
        {
            var recipe = await _recipeRepository.GetRecipeAsync(recipeId);
            var comments = recipe.Comments;
            comments.OrderBy(comment => comment.CreationTime);

            var recipeMapper = new RecipeMapper();
            var commentMapper = new CommentMapper();

            var recipeViewModel = recipeMapper.Map(recipe);
            recipeViewModel.IsDeleted = (HttpContext.User.Identity.IsAuthenticated & 
                recipe.AuthorId == HttpContext.User.Identity.GetUserId()) ? true : false;
            recipeViewModel.Tag = recipeViewModel.Tag.Replace("Не важно", "");

            var commentsViewModels = commentMapper.Map(comments);
            var userId = HttpContext.User.Identity.GetUserId();
            commentsViewModels.ForEach(comment => comment.IsDeleted = comment.AuthorId == userId ? true : false);
            
            ViewBag.Recipe = recipeViewModel;
            ViewBag.Comments = commentsViewModels;
            
            return View();
        }

        [Authorize]
        public ActionResult Create()
        {
            return PartialView("Partial/WriteRecipePartial");
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> Create(WriteRecipeViewModel newRecipe, HttpPostedFileBase inputFile)
        {
            if (ModelState.IsValid)
            {
                var mapper = new DefaultMapper<WriteRecipeViewModel, Recipe>();
                var recipe = mapper.Map(newRecipe);
                recipe.CreationTime = DateTime.Now;
                recipe.AuthorId = HttpContext.User.Identity.GetUserId();
                if (inputFile == null)
                {
                    recipe.ImageName = FilterRecipeTypes.DefaultPhotoName;
                }
                else
                {
                    recipe.ImageName = inputFile.FileName;
                    // Скачать файл на сервер
                    using (var sourceStream = 
                        System.IO.File.Open(_userImageDirectory + inputFile.FileName,FileMode.Create))
                    {
                        await inputFile.InputStream.CopyToAsync(sourceStream);
                    }
                    
                }
                _recipeRepository.AddRecipe(recipe);
                await _recipeRepository.SaveChangesAsync();
            }

            // Перенаправление на страницу пользователя
            var id = HttpContext.User.Identity.GetUserId();
            return RedirectToAction("User", "User", new { userId = id });
        }

        [Authorize]
        public async Task<ActionResult> Delete(int recipeId)
        {
            var recipe = await _recipeRepository.GetRecipeAsync(recipeId);
            if (System.IO.File.Exists(_userImageDirectory + recipe.ImageName) && recipe.ImageName != FilterRecipeTypes.DefaultPhotoName)
            {
                System.IO.File.Delete(_userImageDirectory + recipe.ImageName);
            }

            await _recipeRepository.DeleteRecipeAsync(recipeId);
            await _recipeRepository.SaveChangesAsync();
            return RedirectToAction("User", "User", new { userId = User.Identity.GetUserId() });
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