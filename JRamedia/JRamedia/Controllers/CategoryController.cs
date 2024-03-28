using JRamedia.Data;
using JRamedia.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;

namespace JRamedia.Controllers
{

    [Authorize]
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
        //get
        public IActionResult Create()
        {

            return View();
        }
        //post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The Name can't be the same with display order");
            }
            if (ModelState.IsValid)
            {
                _db.Categories.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "Category created successfully";
                return RedirectToAction("Index");
                
            }
            return View(obj);
        }

        //post
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Edit(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The Name can't be the same with display order");
            }
            if (ModelState.IsValid)
            {
                _db.Categories.Update(obj);
                _db.SaveChanges();
                TempData["success"] = "Category updated successfully";
                return RedirectToAction("Index");
                
            }
            return View(obj);
        }
        
        public IActionResult Edit(int? id)
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }
            var categoryFromDB = _db.Categories.Find(id);
            //var categoryFromDBFirst = _db.Categories.FirstOrDefault(u=>u.Id == id);
            //var categoryFromDBSingle = _db.Categories.SingleOrDefault(u=>u.Id == id);
            if(categoryFromDB == null)
            {
                return NotFound();
            }
            return View(categoryFromDB);
        } 
        [HttpPost, ActionName("Delete")]
        [AutoValidateAntiforgeryToken]
        public IActionResult DeleteCategory(int? id)

        {
            var obj = _db.Categories.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
           _db.Categories.Remove(obj);
            _db.SaveChanges();
            TempData["success"] = "Category deleted successfully";
            return RedirectToAction("Index");
        }
        
        public IActionResult Delete(int? id)
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }
            var categoryFromDB = _db.Categories.Find(id);
            //var categoryFromDBFirst = _db.Categories.FirstOrDefault(u=>u.Id == id);
            //var categoryFromDBSingle = _db.Categories.SingleOrDefault(u=>u.Id == id);
            if(categoryFromDB == null)
            {
                return NotFound();
            }
            return View(categoryFromDB);
        } 
       


    }
}
