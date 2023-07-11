using System;
using System.Collections.Generic;

namespace Repository.GeneratedModels
{
    public partial class UsersInGroup
    {
        public int UserCode { get; set; }
        public int GroupCode { get; set; }
        public string? UserType { get; set; }

        public virtual Group GroupCodeNavigation { get; set; } = null!;
        public virtual User UserCodeNavigation { get; set; } = null!;
    }
}
