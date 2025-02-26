using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; // Import Entity Framework Core for database operations
using E_COMMERCE_Website.Models;
namespace e_commerce.Controllers
{
    public class ProductsController : Controller
    {
        private readonly MyDbContext _context;

        public ProductsController(MyDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var products = _context.Products.ToList();
            return View(products);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Product product)
        {

            _context.Add(product);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int Id)
        {

            var student = _context.Products.Find(Id);

            return View(student);

        }

        [HttpPost]
        public IActionResult Edit(Product product)
        {
            _context.Update(product);
            _context.SaveChanges();

            return RedirectToAction("Index");


        }
        public ActionResult Details(int id)
        {
            var Product = _context.Products.Find(id);
            if (Product == null)
            {
                return NotFound();
            }
            return View(Product);
        }

        public IActionResult Delete(int id)
        {
            var product = _context.Products.Find(id); // find for user data by id in database
            _context.Products.Remove(product); // remove user data from database
            _context.SaveChanges(); // save the delete data for user in database
            return RedirectToAction("Index");
        }
    }
}
