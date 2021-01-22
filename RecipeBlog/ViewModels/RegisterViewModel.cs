using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RecipeBlog.ViewModels
{
    public class RegisterViewModel
    {
        [Required (ErrorMessage = "Введите Ваше имя или псевдоним")]
        [Display(Name = "Имя")]
        [StringLength(256, ErrorMessage = "Макимальная длина имени 256 символов")]
        public string NickName { get; set; }

        [Display(Name = "Описание")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Введите адрес используемой электронной почты")]
        [Display(Name = "Адрес электронной почты")]
        [EmailAddress]
        public string Email { get; set; }

        [StringLength(100, ErrorMessage = "Значение {0} должно содержать не менее {2} символов.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        [Required(ErrorMessage = "Введите пароль")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Повторите пароля")]
        [Compare("Password", ErrorMessage = "Пароль и его подтверждение не совпадают.")]
        public string ConfirmPassword { get; set; }
    }
}