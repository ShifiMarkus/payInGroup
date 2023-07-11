using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repository.GeneratedModels;
using Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DATA.CashData
{
    public class Cashes : ICashes
    {
        private readonly MyDBContext _context;
        private readonly IMapper _mapper;
        public Cashes(MyDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        //public async Task<bool> createCash(CashDto cash)
        //{
        //    var cashModel = _mapper.Map<Cash>(cash);
        //    await _context.AddAsync(cashModel);
        //    var res = await _context.SaveChangesAsync() >= 0;

        //    return res;
        //}

        public async Task<bool> createCash(int groupCode)
        {
            //creae default cash
            CashDto cash = new CashDto();
            cash.GroupCode = groupCode;
            cash.GroupGoal = "";
            cash.GroupSum = 0;
            cash.Deadline = DateTime.Today;
            var cashModel = _mapper.Map<Cash>(cash);
            await _context.AddAsync(cashModel);
            var res = await _context.SaveChangesAsync() >= 0;

            return res;
        }

        public async Task<CashDetailesDto> getCasheDetailes(int cashCode)
        {
            try
            {

                var res = await (from c in _context.Cashes
                                 join p in _context.Payments
                                 on c.CashCode equals p.CashCode

                                 where c.CashCode == cashCode

                                 select new CashDetailesDto
                                 {
                                     GroupGoal = c.GroupGoal,
                                     GroupSum = c.GroupSum,
                                     Deadline = c.Deadline,
                                     countMembers = _context.UsersInGroups.Where(x => x.GroupCode == c.GroupCode).Count(),
                                     sumPaid = c.GroupSum  - p.SumPaid,
                                     paidMembers = _context.UsersInGroups.Where(x => x.UserCode == p.UserCode).Count()
                                 }).FirstOrDefaultAsync();
                return res;
            }catch(Exception ex)
            {

            }

            return null;
        }

        public async Task<List<Cash>> getCashesList(int groupCode)
        {
            var casheList = await _context.Cashes.Where(x => x.GroupCode == groupCode).ToListAsync();
            return casheList;
        }

        public async Task<List<Cash>> getCashesByManagerId(int managerId)
        {
            var subqueryResult =await _context.UsersInGroups.Where(u => u.UserCode == managerId && u.UserType == "manager").Select(u => u.GroupCode).ToListAsync();
            if (subqueryResult != null)
            {
                var result = await _context.Cashes.Where(c => subqueryResult.Contains((int)c.GroupCode)).ToListAsync();
               return result;
            }
            return null;
        }

        public async Task<bool> updateCasheById(int cashCode, CashDetailesDto cashe)
        { 
            var casheId = await _context.Cashes.FirstOrDefaultAsync(x => x.CashCode == cashCode);
            casheId.GroupSum = cashe.paidMembers==null? casheId.GroupSum: cashe.paidMembers;
            casheId.Deadline = cashe.Deadline == null ? casheId.Deadline : cashe.Deadline;
            //casheId. = cashe.paidMembers == null ? casheId.GroupSum : cashe.paidMembers;

            var isOk = await _context.SaveChangesAsync()>=0;
            return isOk;

        }

        public async Task<bool> updateCashSettingsByGroupId(int groupId, int cashCode, CashDetailesDto cashDetailesDto)
        {
            var group = await _context.Cashes.Where(x => x.GroupCode == groupId).FirstOrDefaultAsync();

            if (group == null)
            {
                return false;
                //return NotFound();
            }
            var cash = await _context.Cashes.Where(x => x.GroupCode == groupId && x.CashCode == cashCode).FirstOrDefaultAsync();
            if(cash == null)
            {
                return false;
               // return NotFound();
            }
            // Update the group settings entity with the values from the DTO
           cash.GroupGoal = cashDetailesDto.GroupGoal;
            cash.Deadline = cashDetailesDto.Deadline;
            cash.GroupSum = cashDetailesDto.GroupSum;
            //cash.GroupDivisionMethod = cashDetailesDto.

            // Save the changes to the database
            await _context.SaveChangesAsync();
            return true;
            //return Ok();
        }

        //פונקציה לקבלת הסכום שנותר לתשלום בקופה
        //get(cashId)
        //sum_to_pay=select group_sum from cashes where(x=>x.cash_code == cashId)
        //sum+paid=select sum(sum_paid) from payments where(x=>x.cash_code == cashId)
        //return sum_to_pay-sum_paid;
       
        public async Task<List<MemberCashDto>> getDataToDownload(int cash, int groupId)
        {
            var res = await (from u in _context.Users
                             join uig in _context.UsersInGroups
                             on u.UserCode equals uig.UserCode

                             //join c in _context.Cashes
                             where uig.GroupCode == groupId
                             select new MemberCashDto
                             {
                                 name = u.FirstName + " " + u.LastName,
                                 paid = _context.Payments.Where(x => x.CashCode == cash && x.UserCode == u.UserCode).Sum(x => x.SumPaid),
                                 haveToPay = 5//_context.Payments.Where(x => x.CashCode == cash && x.UserCode == u.UserCode).OrderByDescending(x => x.PaymentDate).Select(x => new { SumToPay = x.SumToPay })
                             }
                             ).ToListAsync();
            return res;
            //return null;
        }

        public async Task<bool> createCash(int groupCode, CashDetailesDto cash)
        {
            cash.GroupCode = groupCode;
            var cashModel = _mapper.Map<Cash>(cash);
            await _context.AddAsync(cashModel);
            var res = await _context.SaveChangesAsync() >= 0;

            return res;
        }

    }
}