using JRamedia.Data;
using JRamedia.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Permissions;
using static System.Collections.Specialized.BitVector32;

namespace JRamedia.Controllers
{
    public class BookController : Controller
    {
        private readonly ApplicationDBContext _db;
        public BookController(ApplicationDBContext db)
        {
            _db = db;
            
        }
        public IActionResult Index()
        {
            IEnumerable<Book> books = _db.Books;
            return View(books);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Book obj) { 
            if(obj.Title.Length < 5)
            {
                ModelState.AddModelError("Title", "The title length can't be less than 5 characters");
            }
            if(ModelState.IsValid)
            {
                _db.Books.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "Book Added Successfuly";
                return RedirectToAction("Index");
            }
            return View(obj);
        }
    }
}