using Matcher.DataAccess;
using Matcher.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

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
            var id = HttpContext.Session.GetInt32("curUser") ?? -1;
            if (id > 0)
            {
				User curUser = userDAO.Get(id);
                ViewBag.User = curUser;
                List<User> MatchUsers = userDAO.GetListMatchUserByUserID(curUser.UserId);
                User user = userDAO.GetUnMatchUserByUserID(curUser.UserId, 0);
                ViewBag.UserSwipe = user;
                ViewBag.skip = 0;
				return View(MatchUsers);
            }
            else
            {
                return RedirectToAction("Login", "Users");
            }
        }

		public JsonResult Like(int id, int skip)
		{
			UserDAO userDAO = new UserDAO(_context);
			int CurId = HttpContext.Session.GetInt32("curUser") ?? -1;
			if (CurId > 0)
			{
				User curUser = userDAO.Get(CurId);
				if (curUser.UserType.Equals("FREE") && _context.LikeLimits.Where(u => u.UserId == CurId).Select(l => l.LikesToday).First() <= 0)
				{
					return Json(new { status = false });
				}
				MatchesDAO matchesDAO = new MatchesDAO(_context);
                matchesDAO.Add(new Match
                {
                    UserId = CurId,
                    MatchedUserId = id,
                    DateMatched = DateTime.Now
                });
                bool IsMatch = _context.Matches.Where(u => u.MatchedUserId == CurId && u.UserId==id).ToList().Any();
                User UserMatch = new User();
                if (IsMatch) { 
                    UserMatch = userDAO.Get(id); 
                    _context.Messages.Add(new Message { FromUserId = CurId, ToUserId = id, MessageText = "Hi!", DateSent = DateTime.Now });
                    _context.SaveChanges();
                }
				User user = userDAO.GetUnMatchUserByUserID(curUser.UserId, skip);
                UserPhotosDAO userPhotosDAO = new UserPhotosDAO(_context);
				List<string> photosUser = userPhotosDAO.GetListPhotosByUsrID(user.UserId);
				LikeLimit likeLimit = _context.LikeLimits.Where(u => u.UserId == CurId).First();
				likeLimit.DailyLikeLimit -= 1;
				_context.LikeLimits.Update(likeLimit);
				_context.SaveChanges();
				return Json(new { userid = user.UserId, data = photosUser, skip = skip, status = true, name = user.Name, des = user.Description, age = DateTime.Today.Year - user.DateOfBirth?.Year, isMatch = IsMatch, userMatch = new { name=UserMatch.Name, avt= userPhotosDAO.GetAvatarByUsrID(UserMatch.UserId) } });
			}
			else
			{
				return Json(new {status = false});
			}
		}

		public JsonResult Nope(int id, int skip)
		{
			UserDAO userDAO = new UserDAO(_context);
			int CurId = HttpContext.Session.GetInt32("curUser") ?? -1;
			if (CurId > 0)
			{
				User curUser = userDAO.Get(CurId);
				skip+=1;
				User user = userDAO.GetUnMatchUserByUserID(curUser.UserId, skip);
				UserPhotosDAO userPhotosDAO = new UserPhotosDAO(_context);
				List<string> photosUser = userPhotosDAO.GetListPhotosByUsrID(user.UserId);
				return Json(new {userid= user.UserId, data = photosUser, skip=skip, status = true, name= user.Name, des=user.Description, age = DateTime.Today.Year - user.DateOfBirth?.Year });
			}
			else
			{
				return Json(new { status = false });
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
                User user = _context.Users.Where(u => u.Email.Equals(userLoginModel.Email)).FirstOrDefault();
               
                if (user != null)
                {
                    // Retrieve the user's IP address
                    string userIpAddress = HttpContext.Connection.RemoteIpAddress.ToString();

                    user.Ipaddress = userIpAddress;
                    _context.SaveChanges();
                    IpbannedDAO ipbannedDAO = new IpbannedDAO(_context);
                    // Check the ip existence in banned ip
                    if (ipbannedDAO.Get(userIpAddress) != null)
                    {
                        ModelState.AddModelError("", "You have been banned from website");
                        return View();
                    }
                    if (user.Status == false)
                    {
                        ModelState.AddModelError("", "You have been deactivate from website");
                        return View();
                    }

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
        public async void DeleteConfirmed(int id)
        {
            if (_context.Users == null)
            {
                return;
            }
            UserDAO userDAO = new UserDAO(_context);
            User user = userDAO.Get(id);
            if (user != null)
            {
                userDAO.Delete(user);
            }
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
                    // Retrieve the ip address of user
                    IpbannedDAO ipbannedDAO = new IpbannedDAO(_context);
                    string userIpAddress = HttpContext.Connection.RemoteIpAddress.ToString();
                    user.Ipaddress = userIpAddress;
                    if (ipbannedDAO.Get(userIpAddress) != null)
                    {
                        ModelState.AddModelError("", "You have been banned from website");
                        return View();
                    }

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
        public ActionResult GetPage(String pageNumber)
        {
            UserDAO userDAO = new UserDAO(_context);
            List<User> users = userDAO.GetPaginatedList(pageNumber);
            IpbannedDAO ipbannedDAO = new IpbannedDAO(_context);
            List<UserManageModel> list = new List<UserManageModel>();
            foreach (User user in users)
            {
                UserManageModel userManageModel = new UserManageModel(user);
                if (ipbannedDAO.Get(userManageModel.Ipaddress) != null) {
                    userManageModel.IsBanIP = true;
                } else
                {
                    userManageModel.IsBanIP = false;
                }
                list.Add(userManageModel);
            }

            return Json(list);
        }
        [HttpPost]
        public async void Disable(int id)
        {
            if (_context.Users == null)
            {
                return;
            }
            UserDAO userDAO = new UserDAO(_context);
            User user = userDAO.Get(id);
            user.Status = false;
            if (user != null)
            {
                userDAO.Update(user);
            }
        }
        [HttpPost]
        public async void Enable(int id)
        {
            if (_context.Users == null)
            {
                return;
            }
            UserDAO userDAO = new UserDAO(_context);
            User user = userDAO.Get(id);
            user.Status = true;
            if (user != null)
            {
                userDAO.Update(user);
            }
        }


        [HttpPost]
        public async void BanIP(String ipAddress)
        {
            if (_context.Ipbanneds == null)
            {
                return;
            }
            IpbannedDAO ipbannedDAO = new IpbannedDAO(_context);
            ipbannedDAO.Add(ipAddress);

        }
        [HttpPost]
        public async void UnbanIP(String ipAddress)
        {
            if (_context.Ipbanneds == null)
            {
                return;
            }
            IpbannedDAO ipbannedDAO = new IpbannedDAO(_context);
            ipbannedDAO.Remove(ipAddress);

        }
    }
}