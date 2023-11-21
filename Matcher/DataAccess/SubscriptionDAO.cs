using Matcher.Models;
using System.Data.SqlTypes;

namespace Matcher.DataAccess
{
    public class SubscriptionDAO
    {
        private readonly VoVoContext _context;
        public SubscriptionDAO(VoVoContext context)
        {
            _context = context;
        }
        public List<Subscription> GetAll()
        {
            return _context.Subscriptions.ToList();
        }
        public decimal GetTotalRevenue()
        {
            decimal sqlMoney = 0;
            foreach (var subcription in GetAll())
            {
                sqlMoney += (decimal)subcription.TotalCost;

            }
            return sqlMoney;
        }
        public List<Subscription> GetPaginatedList(String pageNumber)
        {
            return _context.Subscriptions.Skip((int.Parse(pageNumber) - 1) * 10).Take(10).ToList();
        }

        public int GetMaximumPagination()
        {
            return ((_context.Subscriptions.Count() % 10) == 0) ? _context.Subscriptions.Count() / 10 : (_context.Subscriptions.Count() / 10) + 1;
        }

    }
}
