using Matcher.DataAccess;
using Matcher.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Matcher.Controllers
{
    public class UsersController : Controller
    {
        private readonly VoVoContext _context;

        public UsersController(VoVoContext context)
        {
            _context = context;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            return _context.Users != null ?
                        View(await _context.Users.ToListAsync()) :
                        Problem("Entity set 'VoVoContext.Users'  is null.");
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        public IActionResult Swipe()
        {
            UserDAO userDAO = new UserDAO(_context);
            User curUser = null;
            var id = HttpContext.Session.GetInt32("curUser") ?? -1;
            if (id != null && id > 0)
            {
                curUser = userDAO.Get(id);
                ViewBag.User = curUser;
                List<User> MatchUsers = userDAO.GetListMatchUserByUserID(curUser.UserId);
                User user = userDAO.GetUnMatchUserByUserID(curUser.UserId, 0);
                ViewBag.UserSwipe = user;

				return View(MatchUsers);
            }
            else
            {
                return RedirectToAction("Login", "Users");
            }
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }

        // GET: Users/Login
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(UserLoginModel userLoginModel)
        {
            if (ModelState.IsValid)
            {
                User user = _context.Users.Where(u => u.Email.Equals(userLoginModel.Email)).First();
               
                if (user != null)
                {
                    // Retrieve the user's IP address
                    string userIpAddress = HttpContext.Connection.RemoteIpAddress.ToString();

                    user.Ipaddress = userIpAddress;
                    _context.SaveChanges();

                    HttpContext.Session.SetInt32("curUser", user.UserId);
                    return RedirectToAction("Swipe", "Users");
                }
                else
                {

                    ModelState.AddModelError("", "Invalid UserName or Password");
                    return View();
                }
            }
            else
            {
                return View(userLoginModel);
            }
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,Name,DateOfBirth,Gender,Location,Description,Email,Password,UserType")] User user)
        {
            if (ModelState.IsValid)
            {
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Users == null)
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
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserId,Name,DateOfBirth,Gender,Location,Description,Email,Password,UserType")] User user)
        {
            if (id != user.UserId)
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
                    if (!UserExists(user.UserId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        public async Task<IActionResult> Logout()
        {
            HttpContext.Session.SetInt32("curUser", -1);
            return View("Login");
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Users == null)
            {
                return Problem("Entity set 'VoVoContext.Users'  is null.");
            }
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int id)
        {
            return (_context.Users?.Any(e => e.UserId == id)).GetValueOrDefault();
        }
        public async Task<IActionResult> Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(int id, [Bind("Name,DateOfBirth,Gender,Location,Description,Email,Password")] User user)
        {
            user.Password = Request.Form["pass"];
            List<User> list = _context.Users.ToList();
            foreach(User item in list)
            {
                if(item.Email == user.Email)
                {
                    ViewBag.error = "Email already registered";
                    return View();
                }
            }
            if (ModelState.IsValid)
            {
                if (user != null)
                {
                    _context.Users.Add(user);
                    _context.SaveChanges();
                    return RedirectToAction("Login", "Users");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid");
                    return View();
                }
            }
            else
            {
                return View();
            }
        }
    }
}