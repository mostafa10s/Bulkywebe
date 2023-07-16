
using Bulky.Utility;
using BulkyBook.DateAccess.Data;
using BulkyBook.DateAccess.Repository;
using BulkyBook.DateAccess.Repository.IRepository;
using BulkyBook.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookweb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles =SD.Role_Admin)]
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository datebase;

        public CategoryController(ICategoryRepository db)
        {

            datebase = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Category> listcatgory = datebase.GetAll();
            // ViewData["age"] = 5;
            return View(listcatgory);
        }
        public IActionResult Create()
        {

            return View();
        }
        [HttpPost]
        public IActionResult Create(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "you havve a probelm here");
            }
            if (ModelState.IsValid)
            {
                datebase.Add(obj);
                datebase.save();
                TempData["success"] = "a success crate";
                return RedirectToAction("Index");
            }
            return View();

        }
        public IActionResult Edit(int? id)
        {
            if (id == null && id == 0)
            {
                return NotFound();
            }
            Category findId = datebase.Get(u => u.Id == id);
            if (findId == null)
            {
                return NotFound();
            }
            return View(findId);
        }
        [HttpPost]
        public IActionResult Edit(Category obj)
        {

            if (ModelState.IsValid)
            {
                datebase.Update(obj);
                datebase.save();
                TempData["success"] = "success Update";
                return RedirectToAction("Index");
            }
            return View();

        }
        public IActionResult Delete(int? id)
        {
            if (id == null && id == 0)
            {
                return NotFound();
            }
            Category findId = datebase.Get(u => u.Id == id);
            if (findId == null)
            {
                return NotFound();
            }
            return View(findId);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult Deletepost(int? id)
        {
            Category categoryDelete = datebase.Get(u => u.Id == id);
            if (categoryDelete == null)
            {
                return NotFound();
            }
            datebase.Remove(categoryDelete);
            datebase.save();
            TempData["success"] = " a success Delete";
            return RedirectToAction("Index");
        }

    }
}
