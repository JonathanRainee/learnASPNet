using JRamedia.Data;
using JRamedia.Models;
using Microsoft.AspNetCore.Mvc;

namespace JRamedia.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDBContext _db;

        public CategoryController(ApplicationDBContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<Category> objCategoryList = _db.Categories;
            return View(objCategoryList);
        }
        
        public IActionResult Create()
        {

            return View();
        }


    }
}
