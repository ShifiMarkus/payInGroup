using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DTO
{
    public class GroupsDetailsDto
    {
        public string? GroupDescription { get; set; }
        public string? GroupType { get; set; }
        public int? membersInGroup { get; set; }
        public int?  GroupCode { get; set; }

    }
}
