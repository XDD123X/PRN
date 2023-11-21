using System;
using System.Collections.Generic;

namespace Matcher.Models
{
    public partial class LikeLimit
    {
        public int UserId { get; set; }
        public int? LikesToday { get; set; }
        public int? DailyLikeLimit { get; set; }

        public virtual User User { get; set; } = null!;
    }
}
