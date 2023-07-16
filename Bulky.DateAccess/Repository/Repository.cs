using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using BulkyBook.DateAccess.Data;
using BulkyBook.DateAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace BulkyBook.DateAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplactionContext _db;
        internal DbSet<T> dbSet;
        public Repository(ApplactionContext db)
        {
            _db = db;
            dbSet = _db.Set<T>();
            _db.Products.Include(u => u.Category).Include(u => u.CategoryId);

        }

        public Repository()
        {

        }
        public void Add(T entity)
        {
            dbSet.Add(entity);
        }

        public T Get(Expression<Func<T, bool>> Filter, string? includeProperties = null )
        {
            IQueryable<T> query = dbSet;
            query = query.Where(Filter);
            if (!string.IsNullOrWhiteSpace(includeProperties))
            {
                foreach (var includePropert in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includePropert);
                }
            }
            return query.FirstOrDefault();
        }

        public IEnumerable<T> GetAll(string? includeProperties = null )
        {
            IQueryable<T> query = dbSet;
            if (!string.IsNullOrWhiteSpace(includeProperties))
            {
                foreach (var includePropert in includeProperties.Split(new char[] {','}, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includePropert);
                }
            }
            return query;
        }

        public void Remove(T entity)
        {
            dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entity)
        {
            dbSet.RemoveRange(entity);
        }
    }
}
