namespace Matcher.Models
{
    public partial class Match
    {
        public int MatchId { get; set; }
        public int? UserId { get; set; }
        public int? MatchedUserId { get; set; }
        public DateTime? DateMatched { get; set; }

        public virtual User? MatchedUser { get; set; }
        public virtual User? User { get; set; }
    }
}