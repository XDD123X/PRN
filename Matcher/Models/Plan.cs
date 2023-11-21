using System;
using System.Collections.Generic;

namespace Matcher.Models
{
    public partial class Plan
    {
        public Plan()
        {
            Subscriptions = new HashSet<Subscription>();
        }

        public int PlanId { get; set; }
        public string PlanName { get; set; } = null!;
        public decimal Amount { get; set; }
        public int Duration { get; set; }

        public virtual ICollection<Subscription> Subscriptions { get; set; }
    }
}
