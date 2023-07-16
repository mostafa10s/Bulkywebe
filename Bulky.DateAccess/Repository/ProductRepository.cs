using BulkyBook.DateAccess.Data;
using BulkyBook.DateAccess.Repository.IRepository;
using BulkyBook.Models;
using Microsoft.Build.Tasks.Deployment.Bootstrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Product = BulkyBook.Models.Product;

namespace BulkyBook.DateAccess.Repository
{

    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly ApplactionContext _db;
        public ProductRepository(ApplactionContext db) : base(db)
        {
            _db = db;

        }
       

        public void save()
        {
            _db.SaveChanges();
        }

        public void Update(Product obj)
        {
         var objFromDb=  _db.Products.FirstOrDefault(u=>u.Id==obj.Id);
            if (objFromDb != null)
            {
                objFromDb.price = obj.price;
                objFromDb.Titel = obj.Titel;
                objFromDb.Auther = obj.Auther;
                objFromDb.price100 = obj.price100;
                objFromDb.price50 = obj.price50;
                objFromDb.ISBN = obj.ISBN;
                objFromDb.Description = obj.Description;
                objFromDb.CategoryId = obj.CategoryId;
                if (objFromDb.ImageUrl != null) {
                    objFromDb.ImageUrl = obj.ImageUrl;
                }
               
            }
        }
    }
}

