using System;
using System.Collections.Generic;

namespace Matcher.Models
{
    public partial class Statistic
    {
        public int StatisticId { get; set; }
        public string Name { get; set; } = null!;
        public string? Value { get; set; }
    }
}
