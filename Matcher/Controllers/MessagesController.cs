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
        public ActionResult Index(int id, int searchid)
        {
            int inlogin = (int)HttpContext.Session.GetInt32("curUser");
            User userlogin = _context.Users.FirstOrDefault(x => x.UserId == inlogin);
            ViewBag.user = userlogin;
            
                List<Message> messages = _context.Messages.Where(x => (x.ToUserId == userlogin.UserId && x.FromUserId == id)
            || (x.ToUserId == id && x.FromUserId == userlogin.UserId)).OrderBy(x => x.MessageId).ToList();
                ViewBag.messages = messages;
                ViewBag.id = id;
            if(searchid == null || searchid == 0)
            {
                List<int?> usermatch = _context.Messages.Where(x => x.FromUserId == inlogin).Select(x => x.ToUserId).Distinct().ToList();
                ViewBag.messid = usermatch;
            }
            else
            {
                List<int?> usermatch = _context.Messages.Where(x => x.FromUserId == inlogin && x.ToUserId == searchid).Select(x => x.ToUserId).Distinct().ToList();
                ViewBag.messid = usermatch;
            }
            
            return View();
        }
        
        public ActionResult saveMess(int userid, int toUserId, string messagetext) 
        {
            if(messagetext != null && !messagetext.Equals(" "))
            {
                Message message = new Message
                {
                    FromUserId = userid,
                    ToUserId = toUserId,
                    MessageText = messagetext,
                    DateSent = DateTime.Now
                };
                _context.Messages.Add(message);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index");
            }
            
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
