using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DTO
{
    public class UsersInGroupDto
    {
        public int UserCode { get; set; }
        public int GroupCode { get; set; }
        public string? UserType { get; set; }
        public string? Mail { get; set; }
    }
}
