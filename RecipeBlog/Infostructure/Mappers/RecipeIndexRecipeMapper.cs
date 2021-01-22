using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using RecipeBlog.Models;
using RecipeBlog.ViewModels;

namespace RecipeBlog.Infostructure.Mappers
{
    public class RecipeIndexRecipeMapper: MapperBase<Recipe, RecipeIndexViewModel>
    {
        public RecipeIndexRecipeMapper()
        {
            _mapper = new Mapper(new MapperConfiguration(
                cfg => cfg.CreateMap<Recipe, RecipeIndexViewModel>().
                    ForMember("AuthorName", opt => opt.MapFrom(src => src.Author.NickName)).
                    ForMember("DateTime", opt => opt.MapFrom(src => src.CreationTime)).
                    ForMember("AuthorId", opt => opt.MapFrom(src => src.Author.Id))));
        }
    }
}