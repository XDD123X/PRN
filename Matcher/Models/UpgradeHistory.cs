using System;
using System.Collections.Generic;

namespace Matcher.Models
{
    public partial class UpgradeHistory
    {
        public int UserId { get; set; }
        public int? PlanId { get; set; }
        public decimal TotalCost { get; set; }
        public DateTime UpgradeDate { get; set; }

        public virtual Plan? Plan { get; set; }
        public virtual User User { get; set; } = null!;
    }
}
