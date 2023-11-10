using Matcher.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Matcher.Controllers
{
    public class MessagesController : Controller
    {
        private readonly VoVoContext _context;
        public MessagesController(VoVoContext context) 
        {
            _context = context;
        }
        // GET: MessagesController
        public ActionResult Index()
        {
            int inlogin = (int)HttpContext.Session.GetInt32("curUser");
            User userlogin = _context.Users.FirstOrDefault(x => x.UserId == inlogin);
            ViewBag.user = userlogin;
            if(userlogin.UserId == 1)
            {
                List<Message> messages = _context.Messages.Where(x => (x.ToUserId == userlogin.UserId && x.FromUserId == 2)
            || (x.ToUserId == 2 && x.FromUserId == userlogin.UserId)).OrderBy(x => x.MessageId).ToList();
                ViewBag.messages = messages;
            }
            else
            {
                List<Message> messages = _context.Messages.Where(x => (x.ToUserId == userlogin.UserId && x.FromUserId == 1)
            || (x.ToUserId == 1 && x.FromUserId == userlogin.UserId)).OrderBy(x => x.MessageId).ToList();
                ViewBag.messages = messages;
            }
                
            return View();
        }

        // GET: MessagesController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: MessagesController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MessagesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: MessagesController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: MessagesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: MessagesController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: MessagesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
