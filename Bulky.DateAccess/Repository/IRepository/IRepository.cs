﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DateAccess.Repository.IRepository
{
    public interface IRepository <T> where T : class
    {
        IEnumerable<T> GetAll (string? includeProperties = null);
        T Get(Expression<Func<T, bool>> Filter, string? includeProperties = null);

        void Add(T entity);
      
        void Remove (T entity);

        void RemoveRange(IEnumerable<T> entity);
       
    }
}
