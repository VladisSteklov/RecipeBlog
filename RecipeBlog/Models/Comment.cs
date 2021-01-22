using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RecipeBlog.Models
{
    public class Comment
    {
        public int Id { get; set; }

        [Required]
        public string CommentBody { get; set; }
        public DateTime CreationTime { get; set; }

        [Required]
        [Column("ApplicationUserId")]
        public string CommentatorId { get; set; }
        [ForeignKey(nameof(CommentatorId))]
        public virtual ApplicationUser Commentator { get; set; }

        [Required]
        public int RecipeId { get; set; }
        public virtual Recipe Recipe { get; set;}

        [Column("CommentId")]
        public int? AnswerId { get; set; }
        [ForeignKey(nameof(AnswerId))]
        public virtual Comment Answer { get; set; }
        
    }
}