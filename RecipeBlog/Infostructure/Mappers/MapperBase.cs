using AutoMapper;
using AutoMapper.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecipeBlog.Infostructure.Mappers
{
    public abstract class MapperBase<T, K>
    {
        protected Mapper _mapper;
        public K Map(T obj)
        {
            return _mapper.Map<T, K>(obj);
        }
        public ICollection<K> Map(ICollection<T> objects)
        {
            return _mapper.Map<ICollection<K>>(objects);
        }
    }
}