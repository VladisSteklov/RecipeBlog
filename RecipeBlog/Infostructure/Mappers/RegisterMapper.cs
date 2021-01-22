using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RecipeBlog.ViewModels;
using RecipeBlog.Models;
using AutoMapper;

namespace RecipeBlog.Infostructure.Mappers
{
    public class RegisterMapper: MapperBase<RegisterViewModel, ApplicationUser>
    {
        public RegisterMapper()
        {
            _mapper = new Mapper(new MapperConfiguration(
                cfg => cfg.CreateMap<RegisterViewModel, ApplicationUser>().ForMember(
                    "UserName", opt => opt.MapFrom(src => src.Email))));
        }
    }
}