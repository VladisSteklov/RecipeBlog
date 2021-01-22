using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RecipeBlog.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Укажите адрес электронной почты")]
        [Display(Name = "Адрес электронной почты")]  
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Укажите пароль учетной записи")]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Display(Name = "Запомнить меня")]
        public bool RememberMe { get; set; }
    }
}