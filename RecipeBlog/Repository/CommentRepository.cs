using Microsoft.Ajax.Utilities;
using RecipeBlog.Models;
using RecipeBlog.Repository.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web;

namespace RecipeBlog.Repository
{
    public class CommentRepository : BaseRepository, IDisposable
    {
        public async Task DeleteCommentAsync(int id)
        {
            var comment = await _db.Comments.FindAsync(id);
            if (comment != null)
            {
                // Сброс внешнего ключа для всех ответов (каскадное удаление)
                var answers = await Task.Run(() => _db.Comments.Where(answer => answer.AnswerId == id));
                await answers.ForEachAsync(com => com.AnswerId = null);
                // Удаление комментария
                _db.Comments.Remove(comment);
            }
        }
        public void AddComment(Comment comment)
        {
            _db.Comments.Add(comment);
        }
    }
}