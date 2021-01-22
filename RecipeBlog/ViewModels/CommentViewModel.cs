using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecipeBlog.ViewModels
{
    public class CommentViewModel
    {
        public int Id { get; set; }
        public string CommentBody { get; set; }
        public DateTime CreationTime { get; set; }

        public string AuthorId { get; set; }
        public string AuthorName { get; set; }

        public string AnswerAuthorId { get; set; }
        public string AnswerName { get; set; }
        public string AnswerComment { get; set; }
        public DateTime DateTimeAnswer { get; set; }

        public bool IsDeleted { get; set; }

        public int RecipeId { get; set; }
    }
}