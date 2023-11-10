using System;
using System.Collections.Generic;

namespace Matcher.Models
{
    public partial class Report
    {
        public int ReportId { get; set; }
        public int? UserId { get; set; }
        public string? Description { get; set; }
        public bool? Status { get; set; }

        public virtual User? User { get; set; }
    }
}
