using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Repository.GeneratedModels;
using Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DATA.PaymentData
{
    public class Payments : IPayments
    {
        private readonly MyDBContext _context;
        private readonly IMapper _mapper;

        public Payments(MyDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<bool> createPayment(PaymentsDto pay)
        {
            var payModel = _mapper.Map<Payment>(pay);

            await _context.AddAsync(payModel);
            var isOk = await _context.SaveChangesAsync() >= 0;
            if (isOk)
            { return true; }
            return false;
            //throw new NotImplementedException();
        }

        public async Task<List<PaymentsDto>> getRecentActions( int cashId)
        {
            var res = await _context.Payments.Include(x=>x.UserCodeNavigation).Where(x => x.CashCode == cashId)
                .Select(x=>new PaymentsDto 
                {
                    Name=x.UserCodeNavigation.FirstName+ " "+ x.UserCodeNavigation.LastName,
                    PaymentDate=x.PaymentDate,
                    SumPaid=x.SumPaid,
                    PaymentDescription = x.PaymentDescription

                } ).OrderByDescending(x=>x.PaymentDate).Take(10).ToListAsync();
            return res;
        }

    }
}
