using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DTO
{
    public class GroupAndCashDto
    {
        public GroupsDto? groupDetails { get; set; }
        public CashDetailesDto? cashDetails { get; set; }
    }
}
