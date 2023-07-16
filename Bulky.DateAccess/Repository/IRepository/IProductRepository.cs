using BulkyBook.Models;
using Microsoft.Build.Tasks.Deployment.Bootstrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Product = BulkyBook.Models.Product;


namespace BulkyBook.DateAccess.Repository.IRepository
{
    public interface IProductRepository : IRepository<Product>
    {
        void Update(Models.Product obj);
        void save();
    }
}
