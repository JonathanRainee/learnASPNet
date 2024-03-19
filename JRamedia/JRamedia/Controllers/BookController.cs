using JRamedia.Data;
using JRamedia.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Permissions;

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
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }
    }
}
