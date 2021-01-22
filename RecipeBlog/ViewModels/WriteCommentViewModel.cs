using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RecipeBlog.ViewModels
{
    public class WriteCommentViewModel
    {
        public int RecipeId { get; set; }
        public string AuthorName { get; set; }

        [Required(ErrorMessage = "Введите комментарий")]
        public string CommentBody { get; set; }
    }
}