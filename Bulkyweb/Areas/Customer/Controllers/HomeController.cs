using BulkyBook.DateAccess.Repository.IRepository;
using BulkyBook.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BulkyBookweb.Areas.Customer.Controllers
{
    [Area("Customer")]

    public class HomeController : Controller
    {
        private readonly IProductRepository datebase;
        private readonly ICategoryRepository DB;
        private readonly IWebHostEnvironment WebHostEnvironment;

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger , IProductRepository db, ICategoryRepository CDB, IWebHostEnvironment webHostEnvironment)
        {
           
                WebHostEnvironment = webHostEnvironment;
                datebase = db;
                this.DB = CDB;
            
            _logger = logger;
        }

        public IActionResult Index()
        {
            IEnumerable<Product> listproduct = datebase.GetAll(includeProperties: "Category");
            return View(listproduct);
        }
      
        public IActionResult Details(int? productId)
        {
     Product product = datebase.Get(u=>u.Id == productId, includeProperties: "Category");
            return View(product);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}