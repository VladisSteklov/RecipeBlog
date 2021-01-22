using RecipeBlog.Repository.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace RecipeBlog.Repository
{
    public class BaseRepository: IDisposable
    {
        protected ApplicationDbContext _db;
        private bool _disposed = false;

        public BaseRepository()
        {
            _db = new ApplicationDbContext();
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }
            if (disposing)
            {
                _db.Dispose();
                _db = null;
            }
            _disposed = true;
        }
        public virtual async Task SaveChangesAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}