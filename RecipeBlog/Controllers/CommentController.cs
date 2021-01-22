using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using RecipeBlog.Infostructure.Mappers;
using RecipeBlog.Models;
using RecipeBlog.Repository;
using RecipeBlog.Repository.Context;
using RecipeBlog.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using RecipeBlog.Infostructure;

namespace RecipeBlog.Controllers
{
    public class CommentController : Controller
    {
        private CommentRepository _commentRepository;

        public CommentController()
        {
            _commentRepository = new CommentRepository();
        }
        public CommentController(CommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }


        public async Task<ActionResult> Comment(int recipeId)
        {
            var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var user = await userManager.FindByIdAsync(HttpContext.User.Identity.GetUserId());

            return PartialView("Partial/WriteCommentPartial", new WriteCommentViewModel 
            { 
                RecipeId = recipeId,
                AuthorName = user.NickName
            });
        }

        [HttpPost]
        public async Task<ActionResult> Comment(WriteCommentViewModel model)
        {
            if(ModelState.IsValid)
            {
                var mapper = new DefaultMapper<WriteCommentViewModel, Comment>();
                var comment = mapper.Map(model);

                comment.CreationTime = DateTime.Now;
                comment.CommentatorId = HttpContext.User.Identity.GetUserId();
                _commentRepository.AddComment(comment);
                await _commentRepository.SaveChangesAsync();
            }
            return RedirectToAction("Recipe", "Recipe", new { recipeId = model.RecipeId });
        }

        public async Task<ActionResult> Answer(int recipeId, int commentId)
        {

            var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var user = await userManager.FindByIdAsync(HttpContext.User.Identity.GetUserId());

            return PartialView("Partial/WriteCommentAnswerPartial", new WriteCommentAnswerViewModel 
            {
                RecipeId = recipeId,
                AnswerId = commentId,
                AuthorName = user.NickName
            });
        }

        [HttpPost]
        public async Task<ActionResult> Answer(WriteCommentAnswerViewModel model)
        {
            if (ModelState.IsValid)
            {
                var mapper = new DefaultMapper<WriteCommentAnswerViewModel, Comment>();
                var comment = mapper.Map(model);

                comment.CreationTime = DateTime.Now;
                comment.CommentatorId = HttpContext.User.Identity.GetUserId();

                _commentRepository.AddComment(comment);
                await _commentRepository.SaveChangesAsync();
            }
            return RedirectToAction("Recipe", "Recipe", new { recipeId = model.RecipeId });
        }

        public async Task<ActionResult> Delete(int commentId, int recipeId)
        {
            await _commentRepository.DeleteCommentAsync(commentId);
            await _commentRepository.SaveChangesAsync();
            return RedirectToAction("Recipe", "Recipe", new { recipeId = recipeId });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_commentRepository != null)
                {
                    _commentRepository.Dispose();
                    _commentRepository = null;
                }
            }
            base.Dispose(disposing);
        }

    }
}