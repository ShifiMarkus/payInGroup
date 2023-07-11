using System;
using System.Collections.Generic;

namespace Repository.GeneratedModels
{
    public partial class Cash
    {
        public Cash()
        {
            Payments = new HashSet<Payment>();
        }

        public int CashCode { get; set; }
        public int? GroupCode { get; set; }
        public string? GroupGoal { get; set; }
        public int? GroupSum { get; set; }
        public string? GroupDivisionMethod { get; set; }
        public DateTime? Deadline { get; set; }
        public string? ReminderCall { get; set; }
        public string? Frequency { get; set; }

        public virtual Group? GroupCodeNavigation { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }
    }
}
