using System;
using System.Collections.Generic;

namespace Repository.GeneratedModels
{
    public partial class Group
    {
        public Group()
        {
            Cashes = new HashSet<Cash>();
            UsersInGroups = new HashSet<UsersInGroup>();
        }

        public int GroupCode { get; set; }
        public string? GroupDescription { get; set; }
        public string? GroupType { get; set; }

        public virtual ICollection<Cash> Cashes { get; set; }
        public virtual ICollection<UsersInGroup> UsersInGroups { get; set; }
    }
}
