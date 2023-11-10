using Matcher.Models;
using Microsoft.EntityFrameworkCore;

namespace Matcher.DataAccess
{
    public class UserDAO
    {
        private readonly VoVoContext _context;

        public UserDAO(VoVoContext context)
        {
            _context = context;
        }

        public void Add(User user)
        {
            _context.Users.Add(user);
        }

        public void Update(User user)
        {
            _context.Users.Update(user);
        }

        public void Delete(User user)
        {
            _context.Remove(user);
        }

        public User Get(int id)
        {
            return _context.Users.Find(id);
        }

        public List<User> GetAll()
        {
            return _context.Users.ToList();
        }

        public List<User> GetListMatchUserByUserID(int userId)
        {
            MatchesDAO matchesDAO = new MatchesDAO(_context);
			var matched = matchesDAO.GetUsersMatchedByUserID(userId).Select(m=>m.MatchedUserId).Distinct();
            var matching = matchesDAO.GetUsersMatchingByUserID(userId).Select(m=>m.UserId).Distinct();

			var matchedUserIds = matched.Where(m => matching.Contains(m)).Distinct().ToList();

			return _context.Users
			  .Where(u => matchedUserIds.Contains(u.UserId))
			  .ToList();
		}
        public User GetUnMatchUserByUserID(int userID, int skip)
        {
            MatchesDAO matchesDAO = new MatchesDAO(_context);
            var matching = matchesDAO.GetUsersMatchingByUserID(userID).Select(m => m.UserId).Distinct();
            var users = _context.Users.Where(u => u.UserId != userID && !matching.Contains(u.UserId)).Skip(skip).Take(1).ToList();
            return users.Any() ? users.First() : null;
		}
    }
}
