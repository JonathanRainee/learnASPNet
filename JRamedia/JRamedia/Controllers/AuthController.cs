using Microsoft.AspNetCore.Mvc;

namespace JRamedia.Controllers
{
    public class AuthController : Controller
    {
        //GET: Auth/Login
        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }
    }
}
