using Matcher.Models;

namespace Matcher.DataAccess
{
    public class IpbannedDAO
    {

        private readonly VoVoContext _context;

        public IpbannedDAO(VoVoContext context)
        {
            _context = context;
        }
        public List<Ipbanned> GetAll()
        {
            return _context.Ipbanneds.ToList();
        }
        public Ipbanned Get(String ip) {
            return _context.Ipbanneds.FirstOrDefault(pro => pro.BannedIp.Equals(ip));
        }
        public bool Remove(String ip)
        {
            try
            {
                _context.Ipbanneds.Remove(this.Get(ip));
                _context.SaveChanges();
                return true;
            } catch
            {
                return false;
            }
        }
        public bool Add(String ip)
        {
            try
            {
                Ipbanned ipbanned = new Ipbanned
                {
                    BannedIp = ip,
                };
                _context.Ipbanneds.Add(ipbanned);
                _context.SaveChanges();
                return true;
            } catch
            {
                return false;
            }
        }
    }
}
