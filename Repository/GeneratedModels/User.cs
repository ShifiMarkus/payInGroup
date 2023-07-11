using System;
using System.Collections.Generic;

namespace Repository.GeneratedModels
{
    public partial class User
    {
        public User()
        {
            Payments = new HashSet<Payment>();
            UsersInGroups = new HashSet<UsersInGroup>();
        }

        public int UserCode { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? UserId { get; set; }
        public string? UserPassword { get; set; }
        public string? UserMail { get; set; }
        public string? UserPhone { get; set; }

        public virtual ICollection<Payment> Payments { get; set; }
        public virtual ICollection<UsersInGroup> UsersInGroups { get; set; }
    }
}
