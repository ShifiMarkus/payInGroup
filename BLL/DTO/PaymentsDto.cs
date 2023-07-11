using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DTO
{
    public class PaymentsDto
    {
        public string? Name { get; set; }
        public int PayCode { get; set; }
        public int? CashCode { get; set; }
        public int? UserCode { get; set; }
        public int? SumToPay { get; set; }
        public int? SumPaid { get; set; }
        public string? PaymentWay { get; set; }
        public DateTime? PaymentDate { get; set; }
        public bool? Confirmation { get; set; }
        public bool? PaymentDescription { get; set; }
    }
}
