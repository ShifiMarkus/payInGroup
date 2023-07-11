using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repository.GeneratedModels;
using Services.DATA.PaymentData;
using Services.DTO;

namespace payInGroup.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly MyDBContext _context;
        private readonly IPayments _dbStore;


        public PaymentsController(MyDBContext context, IPayments dbStore)
        {
            _context = context;
            _dbStore = dbStore;
        }

        // GET: api/Payments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Payment>>> GetPayments()
        {
          if (_context.Payments == null)
          {
              return NotFound();
          }
            return await _context.Payments.ToListAsync();
        }

       

        // GET: api/Payments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Payment>> GetPayment(int id)
        {
          if (_context.Payments == null)
          {
              return NotFound();
          }
            var payment = await _context.Payments.FindAsync(id);

            if (payment == null)
            {
                return NotFound();
            }

            return payment;
        }

        [HttpGet]
        [Route("/api/Payments/getRecentActions/{groupId}")]
        public async Task<ActionResult<List<PaymentsDto>>> getRecentActions(int groupId)
        {
            if (_context.Payments == null)
            {
                return NotFound();
            }
            var res =  await _dbStore.getRecentActions(groupId);
            if(res == null)
            {
                return NotFound();
            }
            return res;
        }

        // PUT: api/Payments/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPayment(int id, Payment payment)
        {
            if (id != payment.PayCode)
            {
                return BadRequest();
            }

            _context.Entry(payment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PaymentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Payments
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Payment>> PostPayment(PaymentsDto payment)
        {
            var res = await _dbStore.createPayment(payment);
            if (res == false)
            {
                return BadRequest();
            }
            return Ok(res);
        }

        // DELETE: api/Payments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePayment(int id)
        {
            if (_context.Payments == null)
            {
                return NotFound();
            }
            var payment = await _context.Payments.FindAsync(id);
            if (payment == null)
            {
                return NotFound();
            }

            _context.Payments.Remove(payment);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PaymentExists(int id)
        {
            return (_context.Payments?.Any(e => e.PayCode == id)).GetValueOrDefault();
        }
    }
}
