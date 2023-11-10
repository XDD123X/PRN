using System;
using System.Collections.Generic;

namespace Matcher.Models
{
    public partial class User
    {
        public User()
        {
            LikeLikedUsers = new HashSet<Like>();
            LikeLikers = new HashSet<Like>();
            MatchMatchedUsers = new HashSet<Match>();
            MatchUsers = new HashSet<Match>();
            MessageFromUsers = new HashSet<Message>();
            MessageToUsers = new HashSet<Message>();
            Reports = new HashSet<Report>();
        }

        public int UserId { get; set; }
        public string? Name { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Gender { get; set; }
        public string? Location { get; set; }
        public string? Description { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? UserType { get; set; }
        public bool? Status { get; set; }
        public string? Ipaddress { get; set; }

        public virtual ICollection<Like> LikeLikedUsers { get; set; }
        public virtual ICollection<Like> LikeLikers { get; set; }
        public virtual ICollection<Match> MatchMatchedUsers { get; set; }
        public virtual ICollection<Match> MatchUsers { get; set; }
        public virtual ICollection<Message> MessageFromUsers { get; set; }
        public virtual ICollection<Message> MessageToUsers { get; set; }
        public virtual ICollection<Report> Reports { get; set; }
    }
}
