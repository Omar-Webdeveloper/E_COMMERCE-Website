using Microsoft.AspNetCore.Mvc;
using E_COMMERCE_Website.Models;
using System.Linq;

namespace e_commerce.Controllers
{
    public class Login_RegisterController : Controller
    {
        private readonly MyDbContext _context;

        public Login_RegisterController(MyDbContext context)
        {
            _context = context;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(User user, string repeatPassword)
        {
            if (ModelState.IsValid)
            {
                if (user.Password != repeatPassword)
                {
                    ModelState.AddModelError("PasswordMismatch", "Passwords do not match.");
                    return View(user);
                }
                user.Role = "user";
                _context.Users.Add(user);
                _context.SaveChanges();
                return RedirectToAction("Login");
            }
            return View(user);
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(User user)
        {
            var existingUser = _context.Users
                .FirstOrDefault(u => u.Email == user.Email && u.Password == user.Password);
            if (existingUser == null)
            {
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return View(user);
                }
            }
            if(existingUser.Role == "admin")
            {
                HttpContext.Session.SetString("UserName", existingUser.Name);
                return RedirectToAction("Index", "Admin");
            }
            if (existingUser != null)
            {
                HttpContext.Session.SetString("UserName", existingUser.Name);
                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return View(user);
        }
 

        public IActionResult Profile()
        {
            var userName = HttpContext.Session.GetString("UserName");
            var user = _context.Users.FirstOrDefault(u => u.Name == userName);
            return View(user);
        }

        public IActionResult EditProfile(int id)
        {
            var user = _context.Users.Find(id);
            return View(user);
        }

        [HttpPost]
        public IActionResult EditProfile(User user)
        {
            _context.Update(user);
            _context.SaveChanges();
            return RedirectToAction("Profile");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return View();
        }
    }
}
