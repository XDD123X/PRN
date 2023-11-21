using System;
using System.Collections.Generic;

namespace Matcher.Models
{
    public partial class Subscription
    {
        public int SubscriptionId { get; set; }
        public int UserId { get; set; }
        public int PlanId { get; set; }
        public decimal? TotalCost { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? PaymentStatus { get; set; }
        public DateTime CreatedDate { get; set; }

        public virtual Plan Plan { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
