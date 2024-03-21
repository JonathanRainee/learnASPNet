﻿using JRamedia.Data;
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
            if (obj.Title.Length < 5)
            {
                ModelState.AddModelError("Title", "The title length can't be less than 5 characters");
            }
            if (ModelState.IsValid)
            {
                _db.Books.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "Book Added Successfuly";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        public IActionResult Update(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var BookFromDB = _db.Books.Find(id);
            if(BookFromDB == null) { 
                return NotFound();
            }
            return View(BookFromDB);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Update(Book book) { 
        
            if (book.Title.Length < 5) {
                ModelState.AddModelError("Title", "Title length can't be less than 5 characters");
            }
            if(ModelState.IsValid) { 
                _db.Books.Update(book);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }


            return View(book);
        }
    }
}