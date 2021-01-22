using AutoMapper;
using RecipeBlog.Models;
using RecipeBlog.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecipeBlog.Infostructure.Mappers
{
    public class CommentMapper:MapperBase<Comment, CommentViewModel>
    {
        public CommentMapper()
        {
            _mapper = new Mapper(new MapperConfiguration(
                cfg => cfg.CreateMap<Comment, CommentViewModel>().
                    ForMember("AuthorName", opt => opt.MapFrom(src => src.Commentator.NickName)).
                    ForMember("AuthorId", opt => opt.MapFrom(src => src.CommentatorId)).
                    ForMember("AnswerName", opt => opt.MapFrom(src => src.Answer.Commentator.NickName)).
                    ForMember("AnswerComment", opt => opt.MapFrom(src => src.Answer.CommentBody)).
                    ForMember("DateTimeAnswer", opt => opt.MapFrom(src => src.Answer.CreationTime)).
                    ForMember("AnswerAuthorId", opt => opt.MapFrom(src => src.Answer.CommentatorId))));
        }
    }
}