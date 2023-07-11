using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DTO
{
    public class MemberCashDto
    {
        public string name { get; set; }
        public int? paid { get; set; }
        public int? haveToPay { get; set; }

    }
}
