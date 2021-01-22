using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Linq;
using System.Web;

namespace RecipeBlog.Models
{
    public class Recipe
    {
        public int Id { get; set; }

        [MaxLength(50)]
        [Required]
        public string Title { get; set; }

        [MaxLength(256)]
        [Required]
        public string Ingredients { get; set; }

        [Required]
        public string Description { get; set; }

        public int Minutes { get; set; }

        public DateTime CreationTime { get; set; }

        [MaxLength(50)]
        public string Meal { get; set; }

        [MaxLength(50)]
        public string CountryKitchen { get; set; }

        [MaxLength(50)]
        public string ImageName { get; set; }
        
        [Column("ApplicationUserId")]
        public string AuthorId { get; set; }
        [ForeignKey(nameof(AuthorId))]
        public virtual ApplicationUser Author { get; set; }

        public virtual List<Comment> Comments { get; set; }

        public Recipe()
        {
            Comments = new List<Comment>();
        }
    }
}