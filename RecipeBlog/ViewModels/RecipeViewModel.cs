using System;
using System.Collections.Generic;
using System.EnterpriseServices.Internal;
using System.Linq;
using System.Web;

namespace RecipeBlog.ViewModels
{
    public class RecipeViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Minutes { get; set; }
        public string Ingredients { get; set; }
        public string Description { get; set; }
        public string AuthorName { get; set; }
        public DateTime CreationTime { get; set; }
        public string Tag { get; set; }

        public string AuthorId { get; set; }

        public bool IsDeleted { get; set; }

        public string ImageName { get; set; }
    }
}