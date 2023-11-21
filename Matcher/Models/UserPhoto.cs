using System;
using System.Collections.Generic;

namespace Matcher.Models
{
    public partial class UserPhoto
    {
        public int? UserId { get; set; }
        public string? PhotoLink { get; set; }

        public virtual User? User { get; set; }
    }
}
