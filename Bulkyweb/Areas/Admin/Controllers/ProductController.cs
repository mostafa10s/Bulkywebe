
using Bulky.Utility;
using BulkyBook.DateAccess.Data;
using BulkyBook.DateAccess.Repository;
using BulkyBook.DateAccess.Repository.IRepository;
using BulkyBook.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Build.Tasks;
using System.Data;
using System.Security.Claims;

namespace BulkyBookweb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class ProductController : Controller
    {
        private readonly IProductRepository datebase;
        private readonly ICategoryRepository DB;
        private readonly IWebHostEnvironment WebHostEnvironment;
        public ProductController(IProductRepository db, ICategoryRepository CDB, IWebHostEnvironment webHostEnvironment)
        {
            WebHostEnvironment= webHostEnvironment;
            datebase = db;
            this.DB = CDB;   
        }

        public IActionResult Index()
        {
            List<Product> listproduct = datebase.GetAll(includeProperties: "Category").ToList();
            
            return View(listproduct);
        }
        public IActionResult Upsert(int? Id)
        {

            IEnumerable<SelectListItem> listcatgory = DB.GetAll().ToList().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });
            ViewData["Category"] = listcatgory;

            
            //     ViewBag.List = listcatgory;
            if (Id ==0 || Id== null)
            {
               Product a= new();
                //   Create
                return View(new Product());

            }
            else
            {
                // Update
                Product findId = datebase.Get(u => u.Id == Id);

                return View(findId);
            
            }
           
        }
        [HttpPost]
        public IActionResult Upsert(Product obj , IFormFile? file , int? Id
            )
        {
          

            if (ModelState.IsValid)
            {
                string rootPath = WebHostEnvironment.WebRootPath;
                if (file !=null)
                {
                    string fileName= Guid.NewGuid().ToString()+ Path.GetExtension(file.FileName);
                    string ProductPath= Path.Combine(rootPath, @"Images\Product");
                  
                    if (!string.IsNullOrEmpty(obj.ImageUrl))
                    {
                   
                        var oldImagePath = Path.Combine(rootPath, obj.ImageUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }

                    }
                    using (var fileStream = new FileStream(Path.Combine(ProductPath,fileName),FileMode.Create))
                    {
                        file.CopyTo(fileStream);

                    }
                    obj.ImageUrl = @"Images/Product/" + fileName;

                }
                if(obj.Id == 0)
                {
                    datebase.Add(obj);
                }
                else
                {
                    datebase.Update(obj);
                }
               
                datebase.save();
                TempData["success"] = "a success crate";
                return RedirectToAction("Index");
            }
            return View(obj);

        }
        //  public IActionResult Edit(int? id)
        //  {
        //    if (id == null && id == 0)
        //   {
        //      return NotFound();
        //  }
        //  Product findId = datebase.Get(u => u.Id == id);
        //  if (findId == null)
        //  {
        //   return NotFound();
        //   }
        //    return View(findId);
        //  }
        //   [HttpPost]
        //   public IActionResult Edit(Product obj)
        //    {

        //    if (ModelState.IsValid)
        //    {
        //       datebase.Update(obj);
        //        datebase.save();
        //        TempData["success"] = "success Update";
        //        return RedirectToAction("Index");
        //    }
        //  return View();
        // 
        //     }
        //public IActionResult Delete(int? id)
        //{
        //    if (id == null && id == 0)
        //    {
        //        return NotFound();
        //    }
        //    Product findId = datebase.Get(u => u.Id == id);
        //    if (findId == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(findId);
        //}
        //[HttpPost, ActionName("Delete")]
        //public IActionResult Deletepost(int? id)
        //{
        //    Product Delete = datebase.Get(u => u.Id == id);
        //    if (Delete == null)
        //    {
        //        return NotFound();
        //    }
        //    datebase.Remove(Delete);
        //    datebase.save();
        //    TempData["success"] = " a success Delete";
        //    return RedirectToAction("Index");
        //}
        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Product> ObjProducts = datebase.GetAll(includeProperties: "Category").ToList();

            return Json(new {data = ObjProducts });
        }
      
        [HttpDelete]
        public IActionResult Delete(int? Id)
        {
            var deleteProducts = datebase.Get(u=>u.Id ==Id);
            if (deleteProducts == null)
            {
                return Json(new { success = false, message = "Error while delelting" });
            };
            var oldImagePath = Path.Combine(WebHostEnvironment.WebRootPath , deleteProducts.ImageUrl.TrimStart('\\'));
            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }

            datebase.Remove(deleteProducts);
            datebase.save();
            return Json(new { success = true, message = "delelte is success" });
        }

        #endregion

    }
}


