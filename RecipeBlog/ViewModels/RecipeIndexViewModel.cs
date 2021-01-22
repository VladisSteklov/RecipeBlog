using System;
using System.Collections.Generic;
using System.Dynamic;
using System.EnterpriseServices.Internal;
using System.Linq;
using System.Web;

namespace RecipeBlog.ViewModels
{
    public class RecipeIndexViewModel
    {
        // Максимальный размер строки выводимой в представление информации
        private const int _maxLengthIngredients = 275;
        public string Title { get; set; }
        public int Id { get; set; }
        public int Minutes { get; set; }

        private string _ingredients;
        public string Ingredients 
        { 
            get 
            {
                if (_ingredients.Length <= _maxLengthIngredients) return _ingredients;
                return _ingredients.Substring(0, _maxLengthIngredients - 3) + "...";
            }
            set => _ingredients = value;
        }
        public string AuthorName { get; set; }
        public string AuthorId { get; set; }
        public DateTime DateTime { get; set; }

        public string ImageName { get; set; }
    }
}