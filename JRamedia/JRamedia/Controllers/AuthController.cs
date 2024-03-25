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
            User objByUsername = _db.Users.FirstOrDefault(u => user.Email == u.Email);
            if (objByUsername == null)
            {
                ModelState.AddModelError("Email", "Please input a valid email!");
                return View(user);
            }

            User obj = _db.Users.FirstOrDefault(u => user.Email == u.Email && user.Password == u.Password);
            if (obj == null)
            {
                ModelState.AddModelError("Password", "Please input the right credential!");
                return View(user);
            }
            foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
            {
                System.Diagnostics.Debug.WriteLine($"ModelState Error: {error.ErrorMessage}");
            }
            System.Diagnostics.Debug.WriteLine("haha");
            return Redirect("/home/index");
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Register(User user)
        {
            User objByUsername = _db.Users.FirstOrDefault(u => u.Userame == user.Userame);
            if (objByUsername != null)
            {
                ModelState.AddModelError("Userame", "The username has already taken!");
            }
            
            var objByEmail= _db.Users.FirstOrDefault(u => u.Email == user.Email);
            if (objByEmail != null)
            {
                ModelState.AddModelError("Email", "The email has already taken!");
            }
            
            if(user.Password.Length < 8)
            {
                ModelState.AddModelError("Password", "Please provide a strong password!");
            }
            if (ModelState.IsValid)
            {
                _db.Users.Add(user);
                _db.SaveChanges();
                TempData["success"] = "Account Registered Successfuly";
                return RedirectToAction("Login");
            }
            return View(user);
        }
    }
}
