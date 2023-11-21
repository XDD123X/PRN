using System;
using System.Collections.Generic;

namespace Matcher.Models
{
    public partial class Message
    {
        public int MessageId { get; set; }
        public int? FromUserId { get; set; }
        public int? ToUserId { get; set; }
        public string? MessageText { get; set; }
        public DateTime? DateSent { get; set; }

        public virtual User? FromUser { get; set; }
        public virtual User? ToUser { get; set; }
    }
}
