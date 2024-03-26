using JRamedia.Data;
using JRamedia.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Policy;
using System.Text.Json;
using System.Text.Json.Serialization;

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
            var userObj = JsonSerializer.Serialize(obj);
            HttpContext.Session.SetString("user", userObj);

            var cookie = new CookieOptions
            {
                Expires = DateTime.UtcNow.AddSeconds(10),
                HttpOnly = true
            };
            Response.Cookies.Append("cookie", userObj, cookie);
            return Redirect("/home/index");

            //if (Request.Cookies.ContainsKey("cookie"))
            //{
            //    return Redirect("/home/index");
            //}
            //else
            //{
            //    ModelState.AddModelError("Password", "Can't create cookie!");
            //    return View(user);
            //}
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

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            Response.Headers["Cache-Control"] = "no-cache, no-store";
            Response.Headers["Pragma"] = "no-cache";
            Response.Headers["Expires"] = "-1";
            return View("Login");
        }
    }
}
