using RecipeBlog.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls.WebParts;

namespace RecipeBlog.Infostructure.Services
{
    public class FilterCookiesSerivce
    {
        private FilterRecipeViewModel _cookies;
        private FilterRecipeTypes.FILTER_TYPE _type;

        //Cookies Names
        private const string _filter = "filter";
        public FilterCookiesSerivce()
        {
            _cookies = new FilterRecipeViewModel
            {
                Meal = "Не важно",
                CountryKitchen = "Не важно",
                CommentsAvailability = "Не важно",
                PhotoAvailability = "Не важно"
            };
            _type = FilterRecipeTypes.FILTER_TYPE.None;
        }
        public FilterCookiesSerivce(FilterRecipeTypes.FILTER_TYPE type):this(null, type)
        {

        }
        public FilterCookiesSerivce(FilterRecipeViewModel model, FilterRecipeTypes.FILTER_TYPE type)
        {
            _cookies = model;
            _type = type;
        }
        public static FilterCookiesSerivce Create()
        {
            return new FilterCookiesSerivce();
        }

        public FilterRecipeViewModel GetCookiesFilter(HttpContextBase context)
        {
            var cookiesSerivce = FilterCookiesSerivce.Create();

            var keys = context.Request.Cookies.AllKeys;
            if (keys.Contains("meal")) cookiesSerivce._cookies.Meal = context.Request.Cookies["meal"].Value;
            if (keys.Contains("countrykitchen")) cookiesSerivce._cookies.CountryKitchen = context.Request.Cookies["countrykitchen"].Value;
            if (keys.Contains("photoavailability")) cookiesSerivce._cookies.PhotoAvailability = context.Request.Cookies["photoavailability"].Value;
            if (keys.Contains("commentsavailability")) cookiesSerivce._cookies.CommentsAvailability = context.Request.Cookies["commentsavailability"].Value;
            if (keys.Contains(_filter) && context.Request.Cookies[_filter].Value == "filter")
            {
                _type = FilterRecipeTypes.FILTER_TYPE.Filters;
            }         
            return cookiesSerivce._cookies;
        }

        public void SetCookies(HttpContextBase context)
        {
            context.Response.Cookies[_filter].Value = FilterRecipeTypes.filterTypeDict[_type];
            if (_type == FilterRecipeTypes.FILTER_TYPE.Filters && _cookies!= null)
            {
                context.Response.Cookies["meal"].Value = _cookies.Meal;
                context.Response.Cookies["countrykitchen"].Value = _cookies.CountryKitchen;
                context.Response.Cookies["photoavailability"].Value = _cookies.PhotoAvailability;
                context.Response.Cookies["commentsavailability"].Value = _cookies.CommentsAvailability;
            }
        }

        public static bool? CheckFilterBool(string filter)
        {
            bool? result = null;
            if (filter == "Есть") result = true;
            if (filter == "Нет") result = false;

            return result;
        }
    }
}