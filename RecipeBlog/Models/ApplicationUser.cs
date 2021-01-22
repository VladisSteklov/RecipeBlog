using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace RecipeBlog.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [StringLength(256)]
        public string NickName { get; set; }

        public string Description { get; set; }

        public virtual List<Recipe> Recipes { get; set; }
        public virtual List<Comment> Comments { get; set; }

        public ApplicationUser()
        {
            Recipes = new List<Recipe>();
            Comments = new List<Comment>();
        }
    }
}