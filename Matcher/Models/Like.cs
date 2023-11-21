using System;
using System.Collections.Generic;

namespace Matcher.Models
{
    public partial class Like
    {
        public int LikeId { get; set; }
        public int? LikerId { get; set; }
        public int? LikedUserId { get; set; }
        public DateTime? DateLiked { get; set; }

        public virtual User? LikedUser { get; set; }
        public virtual User? Liker { get; set; }
    }
}
