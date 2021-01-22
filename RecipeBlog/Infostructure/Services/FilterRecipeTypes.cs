using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RecipeBlog.Infostructure.Services
{
    public class FilterRecipeTypes
    {
        // Текущий тип используемого фильтра при выборе рецепта пользователем
        // Ипользуется в кукис пользователя
        public enum FILTER_TYPE
        {
            None,
            News,
            Populars,
            Filters
        }

        // Список выбора (select) для фильтрации рецептов

        IEnumerable<SelectListItem> _meals = new SelectList(new string[] { "Не важно", "Второе блюдо","Выпечка", "Гарнир", "Десерты", "Завтрак", "Напитки", "Соусы",
        "Суп" });
        IEnumerable<SelectListItem> _kitchens = new SelectList(new string[] { "Не важно", "Австралийская","Азиатская", "Американская", "Африканская", "Европейская",
        "Русская", "Средеземноморская", "Южноамериканская"});
        public IEnumerable<SelectListItem> Meals
        {
            get
            {
                return _meals;
            }
        }

        public IEnumerable<SelectListItem> Kitchens
        {
            get
            {
                return _kitchens;
            }
        }

        // Имя фото для рецепта по умолчанию
        public static string DefaultPhotoName
        {
            get
            {
                return "recipe.png";
            }
        }

        // Словарь фильтров для кукис
        public static Dictionary<FILTER_TYPE, string> filterTypeDict = new Dictionary<FILTER_TYPE, string>
        {
            { FILTER_TYPE.Filters, "filter" },
            { FILTER_TYPE.News, "news" },
            { FILTER_TYPE.Populars, "populars" },
            { FILTER_TYPE.None, "none" }
        };
    }
}