using JRamedia.Data;
using JRamedia.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Policy;

namespace JRamedia.Controllers
{
    public class AuthController : Controller
    {
        private readonly ApplicationDBContext _db;

        public AuthController(ApplicationDBContext db)
        {
            _db = db;
        }
        //GET: Auth/Login
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Login(User user)
        {
            var obj = _db.Users.Find(user);
            if (obj == null)
            {
                ModelState.AddModelError("User", "Please input a right credential!");
            }
            else
            {

            }
            return View(user);
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Register(User user)
        {
            var objByUsername = _db.Users.Where(u => EF.Functions.Like(u.Userame, $"%{user.Userame}%")).ToList();
            if (objByUsername != null)
            {
                ModelState.AddModelError("Username", "The username has already taken!");
            }
            var objByEmail= _db.Users.Where(u => EF.Functions.Like(u.Email, $"%{user.Email}%")).ToList();
            if (objByEmail != null)
            {
                ModelState.AddModelError("Email", "The email has already taken!");
            }
            if(user.Password.Length < 10)
            {
                ModelState.AddModelError("Password", "Please provide a strong password!");
            }
            _db.Users.Add(user);
            _db.SaveChanges();
            TempData["success"] = "Account Registered Successfuly";
            return RedirectToAction("Login");
        }
    }
}
