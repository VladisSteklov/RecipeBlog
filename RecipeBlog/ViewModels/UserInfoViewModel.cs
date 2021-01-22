using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecipeBlog.ViewModels
{
    public class UserInfoViewModel
    {
        public string NickName { get; set; }
        public string Description { get; set; }
        public bool CanCreateRecipe { get; set; }
    }
}