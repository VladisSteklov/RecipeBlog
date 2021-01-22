using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecipeBlog.Infostructure.Mappers
{
    public class DefaultMapper<T, K> : MapperBase<T,K>
    {
        public DefaultMapper()
        {
            _mapper = new Mapper(new MapperConfiguration(cfg => cfg.CreateMap<T,K>()));
        }
    }
}