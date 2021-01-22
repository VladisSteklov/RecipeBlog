using RecipeBlog.Infostructure.Mappers;
using RecipeBlog.Infostructure.Services;
using RecipeBlog.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace RecipeBlog.Repository
{
    public class RecipeRepository: BaseRepository
    {
        public async Task DeleteRecipeAsync(int id)
        {
            var recipe = await _db.Recipes.FindAsync(id);
            if (recipe != null)
            {
                _db.Recipes.Remove(recipe);
            }
        }
        public void AddRecipe(Recipe recipe)
        {
            _db.Recipes.Add(recipe);
        }
       

        public async Task<Recipe> GetRecipeAsync(int id)
        {
            return await _db.Recipes.FindAsync(id);
        }
        public async Task<List<Recipe>> GetRecipesAsync()
        {
            return await _db.Recipes.ToListAsync();
        }
        public async Task<List<Recipe>> GetNewRecipesAsync()
        {
            var recipes = await Task.Run(() => _db.Recipes.OrderByDescending(recipe => recipe.CreationTime));
            return await recipes.ToListAsync();
        }
        public async Task<List<Recipe>> GetPopularRecipesAsync()
        {
            var recipes = await Task.Run(() => _db.Recipes.OrderByDescending(recipe => recipe.Comments.Count));
            return await recipes.ToListAsync();
        }
        public async Task<List<Recipe>> GetRecipeWithFilterAsync(string meal, string kitchen, bool? photo, bool? comment)
        {
            IQueryable<Recipe> result = _db.Recipes;
            if (meal != "Не важно")
            {
                result = await Task.Run(() => _db.Recipes.Where(recipe => recipe.Meal == meal));
            }
            if (kitchen != "Не важно")
            {
                result = await Task.Run(() => result.Where(recipe => recipe.CountryKitchen == kitchen));
            }
            
            if (photo == null && comment == null)
            {
                return await Task.Run(() => result.OrderByDescending(recipe => recipe.CreationTime).ToListAsync());
            }
            if (photo != null)
            {
                result = await FilterRecipePhotoAsync(result, photo);
            }
            if (comment != null)
            {
                result = await FilterRecipeCommentAsync(result, comment);
            }
            return await result.ToListAsync();
        }
        private async Task<IQueryable<Recipe>> FilterRecipeCommentAsync(IQueryable<Recipe> recipes, bool? comment)
        {
            if (comment == true)
            {
                recipes = await Task.Run(() =>
                    recipes.Where(recipe => recipe.Comments.Count != 0).OrderByDescending(recipe => recipe.CreationTime));
            }
            else if (comment == false)
            {
                recipes = await Task.Run(() =>
                    recipes.Where(recipe => recipe.Comments.Count == 0).OrderByDescending(recipe => recipe.CreationTime));
            }
            return recipes;
        }
        private async Task<IQueryable<Recipe>> FilterRecipePhotoAsync(IQueryable<Recipe> recipes, bool? photo)
        {
            if (photo == true)
            {
                recipes = await Task.Run(() =>
                    recipes.Where(recipe => recipe.ImageName != FilterRecipeTypes.DefaultPhotoName).OrderByDescending(recipe => recipe.CreationTime));
            }
            else if (photo == false)
            {
                recipes = await Task.Run(() =>
                    recipes.Where(recipe => recipe.ImageName == FilterRecipeTypes.DefaultPhotoName).OrderByDescending(recipe => recipe.CreationTime));
            }
            return recipes;
        }
    }
}