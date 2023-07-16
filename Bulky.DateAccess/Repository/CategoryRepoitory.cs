using BulkyBook.DateAccess.Data;
using BulkyBook.DateAccess.Repository.IRepository;
using BulkyBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DateAccess.Repository
{
    public class CategoryRepoitory : Repository<Category>, ICategoryRepository
    {
        private readonly ApplactionContext _db;
        public CategoryRepoitory(ApplactionContext db) : base(db) {
            _db = db;   

        }
        

            
        public void save()
        {
           _db.SaveChanges();
        }

        public void Update(Category obj)
        {
          _db.Categories.Update(obj);
        }
    }
}
