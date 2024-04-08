using JRamedia.Data;
using JRamedia.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Permissions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using static System.Collections.Specialized.BitVector32;
using Microsoft.AspNetCore.Hosting;
using System;
using System.IO;

namespace JRamedia.Controllers
{
    [Authorize]
    public class BookController : Controller
    {
        private readonly IWebHostEnvironment _environment;
        private readonly ApplicationDBContext _db;
        public BookController(IWebHostEnvironment environment, ApplicationDBContext db)
        {
            _environment = environment;
            _db = db;

        }
        public IActionResult Index()
        {
            IEnumerable<Books> books = _db.Books;
            return View(books);
        }

        public IActionResult Create()
        {
            IEnumerable<Category> categories = _db.Categories;
            Books book = new Books();
            return View(Tuple.Create(book, categories));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Books obj, IFormFile Image) {
            IEnumerable<Category> categories = _db.Categories;
            if (obj.Title.Length < 5)
            {
                ModelState.AddModelError("Title", "The title length can't be less than 5 characters");
            }
            if (ModelState.IsValid)
            {
                if (Image != null && Image.Length > 0)
                {
                    var imagePath = Path.Combine(_environment.WebRootPath, "images", Image.FileName);
                    using (var stream = new FileStream(imagePath, FileMode.Create))
                    {
                        Image.CopyTo(stream);
                    }
                    obj.Image = Image.FileName;
                }

                _db.Books.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "Book Added Successfuly";
                return RedirectToAction("Index");
            }
            return View(Tuple.Create(obj, categories));
        }

        public IActionResult Update(int? id)
        {
            IEnumerable<Category> categories = _db.Categories;
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var BookFromDB = _db.Books.Find(id);
            if(BookFromDB == null) { 
                return NotFound();
            }
            return View(Tuple.Create(BookFromDB, categories));
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult UpdateBook(Books book, IFormFile Image) {
            IEnumerable<Category> categories = _db.Categories;

            if (book.Title.Length < 5) {
                ModelState.AddModelError("Title", "Title length can't be less than 5 characters");
            }
            if(ModelState.IsValid) {
                if (Image != null && Image.Length > 0)
                {
                    var imagePath = Path.Combine(_environment.WebRootPath, "images", Image.FileName);
                    using (var stream = new FileStream(imagePath, FileMode.Create))
                    {
                        Image.CopyTo(stream);
                    }
                    book.Image = Image.FileName;
                }
                _db.Books.Update(book);
                _db.SaveChanges();
                TempData["success"] = "Book updated successfully";
                return RedirectToAction("Index");
            }
            return View(book);
        }

        public IActionResult Delete(int? id)
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }
            var book = _db.Books.Find(id);
            if(book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult DeleteBook(int? id)
        {
            var book = _db.Books.Find(id);
            if(book == null)
            {
                return NotFound();
            }
            _db.Books.Remove(book);
            _db.SaveChanges();
            TempData["success"] = "Book Deleted successfully";
            return RedirectToAction("Index");
        }
    }
}