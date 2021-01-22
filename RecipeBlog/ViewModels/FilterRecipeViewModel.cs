using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;


namespace RecipeBlog.ViewModels
{
    public class FilterRecipeViewModel
    {
        [Required(ErrorMessage = "Укажите тип продукта или выберете не важно")]
        [Display(Name = "Тип продукта")]
        public string Meal { get; set; }

        [Required(ErrorMessage = "Укажите страну кухни или выберете не важно")]
        [Display(Name = "Страна кухни")]
        public string CountryKitchen { get; set; }

        [Required(ErrorMessage = "Укажите наличие фото или выберете не важно")]
        [Display(Name = "Наличие фотографии")]
        public string PhotoAvailability { get; set; }

        [Required(ErrorMessage = "Укажите наличие отзывов или выберете не важно")]
        [Display(Name = "Наличие отзывов")]
        public string CommentsAvailability { get; set; }
    }
}