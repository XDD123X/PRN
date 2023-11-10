using Matcher.Models;

namespace Matcher.DataAccess
{
    public class MatchesDAO
    {
        private readonly VoVoContext _context;
        
        public MatchesDAO(VoVoContext context) {  _context = context; }

        public void Add(Match match)
        {
            _context.Add(match);
        }
        public List<Match> GetUsersMatchedByUserID(int userId)
        {
            return _context.Matches.Where(u=>u.UserId == userId).ToList();
        }

        public List<Match> GetUsersMatchingByUserID(int userId)
		{
			return _context.Matches.Where(u => u.MatchedUserId == userId).ToList();
		}
	}
}
