using Matcher.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

namespace Matcher.DataAccess
{
    public class PlanDAO
    {
        VoVoContext _context;
        public PlanDAO(VoVoContext context)
        {
            _context = context;
        }

        public List<Plan> GetAll()
        {
            return _context.Plans.ToList();
        }
        public Plan Get(int id)
        {
            return _context.Plans.FirstOrDefault(x => x.PlanId == id);
        }

        public void Update(Plan setting)
        {
            _context.Plans.Update(setting);
            _context.SaveChanges();
        }
    }
}
