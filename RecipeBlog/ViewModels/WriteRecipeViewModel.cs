using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RecipeBlog.ViewModels
{
    public class WriteRecipeViewModel
    {
        [Required(ErrorMessage = "Напишите название рецепта")]
        [Display(Name = "Название блюда")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Укажите количество минут, требуемое для приготовления блюда")]
        [Display(Name = "Время готовки")]
        public int Minutes { get; set; }

        [Required(ErrorMessage = "Выпишите используемые ингридиенты")]
        [Display(Name = "Ингредиенты")]
        public string Ingredients { get; set; }

        [Required(ErrorMessage = "Опишите шаги приготовки рецепта")]
        [Display(Name = "Подробное описание")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Для улучшения поиска укажите тип продукта")]
        [Display(Name = "Тип продукта")]
        public string Meal { get; set; }

        [Required(ErrorMessage = "Для улучшения поиска укажите страну кухи блюда")]
        [Display(Name = "Стана кухни")]
        public string CountryKitchen { get; set; }
    }
}