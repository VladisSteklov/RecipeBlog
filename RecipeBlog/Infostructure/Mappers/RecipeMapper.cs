using AutoMapper;
using RecipeBlog.Models;
using RecipeBlog.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecipeBlog.Infostructure.Mappers
{
    public class RecipeMapper : MapperBase<Recipe, RecipeViewModel>
    {
        public RecipeMapper()
        {
            _mapper = new Mapper(new MapperConfiguration(
                cfg => cfg.CreateMap<Recipe, RecipeViewModel>().
                    ForMember("AuthorName", opt => opt.MapFrom(src => src.Author.NickName)).
                    ForMember("Tag", opt => opt.MapFrom(src => src.Meal + " " + src.CountryKitchen)).
                    ForMember("AuthorId", opt => opt.MapFrom(src => src.Author.Id))));
        }
    }
}