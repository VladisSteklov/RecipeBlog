using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RecipeBlog.ViewModels
{
    public class WriteCommentAnswerViewModel
    {
        public int RecipeId { get; set; }
        public int AnswerId { get; set; }

        // В форме необходимо также передать Id комментируемого отзыва
        public string AuthorName { get; set; }

        [Required(ErrorMessage = "Введите комментарий")]
        public string CommentBody { get; set; }
    }
}