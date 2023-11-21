using Matcher.Models;
using Matcher.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Matcher.Controllers
{

    public class ProductController : Controller
    {
        private readonly VoVoContext _context;
        private readonly IVnPayService _vnPayService;
        public ProductController(VoVoContext context, IVnPayService vnPayService)
        {
            _context = context;
            _vnPayService = vnPayService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Subscription(string? plan)
        {
            List<Plan> plans = new List<Plan>();

            if (string.IsNullOrEmpty(plan))
            {
                // If the 'plan' parameter is empty, retrieve gold plans
                plans = _context.Plans.Where(x => x.PlanName.ToLower().Contains("gold")).ToList();
            }
            else
            {
                // If a specific plan is provided, retrieve that plan
                plans = _context.Plans.Where(x => x.PlanName.ToLower() == plan.ToLower()).ToList();
            }
            ViewBag.Plans = plans;
            return View();
        }

        public IActionResult CreatePaymentUrl(int PlanID)
        {
            int inlogin = (int)HttpContext.Session.GetInt32("curUser");
            User userlogin = _context.Users.FirstOrDefault(x => x.UserId == inlogin);
            Subscription subscription = new Subscription();
            subscription.PlanId = PlanID;
            subscription.UserId = inlogin;
            subscription.TotalCost = _context.Plans.FirstOrDefault(x => x.PlanId ==  PlanID).Amount;
            subscription.CreatedDate = DateTime.Now;
            _context.Subscriptions.Add(subscription);
            _context.SaveChanges();

            var url = _vnPayService.CreatePaymentUrl(subscription, HttpContext);

            return Redirect(url);
        }

        public IActionResult PaymentCallback()
        {
            var response = _vnPayService.PaymentExecute(Request.Query);

            return Json(response);
        }
    }
   
}
