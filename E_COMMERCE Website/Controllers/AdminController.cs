using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using E_COMMERCE_Website.Models;
namespace E_COMMERCE_Website.Controllers
{
    public class AdminController : Controller
    {
        private readonly MyDbContext _context;

        public AdminController(MyDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var userName = HttpContext.Session.GetString("UserName");
            var user = _context.Users.FirstOrDefault(u => u.Name == userName);
            return View(user);
        }
    }
}
