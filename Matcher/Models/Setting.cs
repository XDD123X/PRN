using System;
using System.Collections.Generic;

namespace Matcher.Models
{
    public partial class Setting
    {
        public int SettingId { get; set; }
        public string Name { get; set; } = null!;
        public string? Value { get; set; }
    }
}
