using System;
using System.Collections.Generic;

namespace Matcher.Models
{
    public partial class Admin
    {
        public int AdminId { get; set; }
        public string? Avatar { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
}
