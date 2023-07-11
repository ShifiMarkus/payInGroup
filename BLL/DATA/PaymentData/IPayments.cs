using Repository.GeneratedModels;
using Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DATA.PaymentData
{
    public interface IPayments
    {
        Task<bool> createPayment(PaymentsDto pay);
        Task<List<PaymentsDto>> getRecentActions( int cashId);
    }
}
