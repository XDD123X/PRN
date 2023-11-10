using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Matcher.Models;
using System.Data;

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
            String adminEmail = HttpContext.Session.GetString("adminEmail");
            Admin admin = _context.Admins.FirstOrDefault(pro => pro.Email == adminEmail);
            if (adminEmail != null)
            {
                ViewData["Admin Name"] = admin.Name;
                ViewData["AvatarUrl"] = admin.Avatar;
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
    }
}
