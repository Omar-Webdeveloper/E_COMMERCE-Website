using Microsoft.AspNetCore.Mvc; // Import the necessary namespaces for ASP.NET Core MVC
using Microsoft.EntityFrameworkCore; // Import Entity Framework Core for database operations
using E_COMMERCE_Website.Models; // Import the models from your project namespace

namespace Task_1.Controllers
{
    public class UserController : Controller
    {
        private readonly MyDbContext _context; // Declare a private readonly field for the database context

        public UserController(MyDbContext context) // Constructor that takes a MyDbContext instance as a parameter
        {
            _context = context; // Assign the passed-in context to the private field
        }


        // GET: Users/All_users
        public async Task<IActionResult> All_users() // Define an action method that returns a Task of IActionResult
        {
            var users = await _context.Users.ToListAsync(); // Retrieve all users from the database asynchronously
            return View(users); // Return the view with the list of users
        }

        // GET: Users/Create
        public IActionResult Create() // Define a GET action method named Create
        {
            return View(); // Return the Create view to display the form for creating a new user
        }

        // POST: Users/Create
        [HttpPost] // Specify that this is a POST action method
        public async Task<IActionResult> Create([Bind("Name,Email")] User user) // Define a POST action method named Create that takes a User object as a parameter
        {
            if (ModelState.IsValid) // Check if the model state is valid (i.e., the data received from the form is valid according to the model's validation rules)
            {
                _context.Add(user); // Add the new user to the database context
                await _context.SaveChangesAsync(); // Save the changes to the database asynchronously
                return RedirectToAction(nameof(All_users)); // Redirect to the All_users action after successfully saving the new user
            }
            return View(user); // If the model state is not valid, return the Create view with the user object to display validation errors
        }


        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }
        // POST: Users/Edit/5
        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Email,Password,Role")] User user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(All_users));
            }
            return View(user);
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }

        // Details Action Method
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(All_users));
        }

    }
}

