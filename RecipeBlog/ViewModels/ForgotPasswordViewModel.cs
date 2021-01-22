using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RecipeBlog.ViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required(ErrorMessage = "Введите адрес электронной почты")]
        [Display(Name = "Адрес электронной почты")]
        [EmailAddress]
        public string Email { get; set; }
    }
}