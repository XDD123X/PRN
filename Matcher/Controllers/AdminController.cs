using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Matcher.Models;
using System.Data;
using Matcher.DataAccess;
using Microsoft.EntityFrameworkCore.ValueGeneration.Internal;

namespace Matcher.Controllers
{
    public class AdminController : Controller
    {
        private readonly VoVoContext _context;

        public AdminController(VoVoContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            UserDAO userDAO = new UserDAO(_context);
            SubscriptionDAO subscriptionDAO = new SubscriptionDAO(_context);
            String adminEmail = HttpContext.Session.GetString("adminEmail");
            Admin admin = _context.Admins.FirstOrDefault(pro => pro.Email == adminEmail);
            if (adminEmail != null)
            {
                ViewData["Admin Name"] = admin.Name;
                ViewData["AvatarUrl"] = admin.Avatar;
                ViewData["Admin Name"] = admin.Name;
                ViewData["AvatarUrl"] = admin.Avatar;
                ViewData["FreeUser"] = userDAO.GetAll().Where(pro => pro.UserType == "FREE").Count();
                ViewData["PremiumUser"] = userDAO.GetAll().Where(pro => pro.UserType == "PREMIUM").Count();
                ViewData["TotalRevenues"] = subscriptionDAO.GetTotalRevenue().ToString("0.00");
                ViewData["TotalUsers"] = userDAO.GetAll().Where(pro => pro.CreatedDate.Month == DateTime.Now.Month && pro.CreatedDate.Year == DateTime.Now.Year).Count();

                return View();
            }
            return RedirectToAction("Login");
        }
        public async Task<IActionResult> Logout()
        {
            HttpContext.Session.Clear();
            return View("Login");
        }
        [Route("Admin/User")]
        public async Task<IActionResult> ListUser()
        {
            String adminEmail = HttpContext.Session.GetString("adminEmail");
            Admin admin = _context.Admins.FirstOrDefault(pro => pro.Email == adminEmail);
            if (adminEmail != null)
            {
                ViewData["Admin Name"] = admin.Name;
                ViewData["AvatarUrl"] = admin.Avatar;
                return View("User");
            }
            return RedirectToAction("Login");
        }

        public async Task<IActionResult> GetAttributeList()
        {
            PlanDAO planDAO = new PlanDAO(_context);
            List<Plan> list = planDAO.GetAll();
            return Json(list);
        }

        public async Task<IActionResult> UpgradeHistory()
        {
            String adminEmail = HttpContext.Session.GetString("adminEmail");
            Admin admin = _context.Admins.FirstOrDefault(pro => pro.Email == adminEmail);
            if (adminEmail != null)
            {
                ViewData["Admin Name"] = admin.Name;
                ViewData["AvatarUrl"] = admin.Avatar;
                return View("Subscription");
            }
            return RedirectToAction("Login");
        }

        [HttpPost]
        public IActionResult UpdateSetting(PlanUpdateModel model)
        {

            PlanDAO planDAO = new PlanDAO(_context);
            Plan temp = planDAO.Get(2);
            //temp.Amount = Decimal.Parse(plan2);
            temp.Amount = model.Plan2;
            planDAO.Update(temp);
            temp = planDAO.Get(3);
            //temp.Amount = Decimal.Parse(plan3);
            temp.Amount = model.Plan3;
            planDAO.Update(temp);
            temp = planDAO.Get(4);
            //temp.Amount = Decimal.Parse(plan4);
            temp.Amount = model.Plan4;
            planDAO.Update(temp);

            ViewData["Message"] = "Successfully update the setting";
            return RedirectToAction("Settings");
        }

        public async Task<IActionResult> Settings()
        {
            String adminEmail = HttpContext.Session.GetString("adminEmail");
            Admin admin = _context.Admins.FirstOrDefault(pro => pro.Email == adminEmail);
            if (adminEmail != null)
            {
                ViewData["Admin Name"] = admin.Name;
                ViewData["AvatarUrl"] = admin.Avatar;
                return View("Plan");
            }
            return RedirectToAction("Login");
        }
        public async Task<IActionResult> Login()
        {
            return View("Login");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(String Email, String Password)
        {
            Admin admin = _context.Admins.FirstOrDefault(x => x.Email == Email);
            if (admin != null)
            {
                if (admin.Password.Equals(Password))
                {
                    HttpContext.Session.SetString("adminEmail", admin.Email);
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewData["ErrorMessage"] = "Wrong password";
                    return View("Login");
                }
            }
            ViewData["ErrorMessage"] = "Email doesn't existed";
            return View("Login");
        }
        public ActionResult GetSubscriptionPage(String pageNumber)
        {
            SubscriptionDAO subcriptionDAO = new SubscriptionDAO(_context);
            List<Subscription> list = subcriptionDAO.GetPaginatedList(pageNumber);
            return Json(list);
        }
    }
}
