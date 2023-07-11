using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DTO
{
    public class CashDetailesDto
    {
        public DateTime? Deadline { get; set; }
        public string? GroupGoal { get; set; }
        public int? GroupSum { get; set; }
        public int? countMembers { get; set; }
        public int? sumPaid { get; set; }
        public int paidMembers { get; set; }
        public int? GroupCode { get; set; }
    }

}
