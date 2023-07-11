using System;
using System.Collections.Generic;

namespace Repository.GeneratedModels
{
    public partial class Payment
    {
        public int PayCode { get; set; }
        public int? CashCode { get; set; }
        public int? UserCode { get; set; }
        public int? SumToPay { get; set; }
        public int? SumPaid { get; set; }
        public string? PaymentWay { get; set; }
        public DateTime? PaymentDate { get; set; }
        public bool? Confirmation { get; set; }
        public bool? PaymentDescription { get; set; }

        public virtual Cash? CashCodeNavigation { get; set; }
        public virtual User? UserCodeNavigation { get; set; }
    }
}
