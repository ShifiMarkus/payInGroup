using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DTO
{
    public class CashDto
    {
        public int CashCode { get; set; }
        public int? GroupCode { get; set; }
        public string? GroupGoal { get; set; }
        public int? GroupSum { get; set; }
        public string? GroupDivisionMethod { get; set; }
        public DateTime? Deadline { get; set; }
        public string? ReminderCall { get; set; }
        public string? Frequency { get; set; }
    }
}
