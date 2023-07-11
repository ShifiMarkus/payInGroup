using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repository.GeneratedModels;
using Services.DATA.CashData;
using Services.DTO;

namespace payInGroup.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CashesController : ControllerBase
    {
        private readonly MyDBContext _context;
        private readonly ICashes _cashesDbStore;

        public CashesController(MyDBContext context, ICashes cashesDbStore)
        {
            _context = context;
            _cashesDbStore = cashesDbStore;
        }

        // GET: api/Cashes
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Cash>>> GetCashes()
        //{
        // var result = await _cashesDbStore.getCashesList()
        //}

        // GET: api/Cashes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Cash>> GetCash(int id)
        {
            var result = await _cashesDbStore.getCashesList(id);
            return Ok(result);
        }
        [HttpGet]
        [Route("/api/Cashes/getCasheDetailes/{cashCode}")]
        public async Task<CashDetailesDto> getCasheDetailes(int cashCode)
        {
            var result = await _cashesDbStore.getCasheDetailes(cashCode);
            return result;
        }

        [HttpGet]
        [Route("/api/Cashes/getCashesByManagerId/{managerId}")]
        public async Task<ActionResult<CashDto>> getCashesByManagerId(int managerId)
            {
            var result = await _cashesDbStore.getCashesByManagerId(managerId);
            return Ok(result);
            }

        [HttpGet]
        [Route("/api/Cashes/getDataToDownload/{Cashe}/{group}")]
        public async Task<ActionResult<CashDetailesDto>> getDataToDownload(int Cashe,int group)
        {
            var result = await _cashesDbStore.getDataToDownload(Cashe,  group);
            return Ok(result);
        }
        // PUT: api/Cashes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{groupId}/{cashId}")]
        public async Task<IActionResult> PutCash(int groupId,int cashId,[FromBody] CashDetailesDto cash)
        {
            var result = await _cashesDbStore.updateCashSettingsByGroupId(groupId, cashId, cash);
            return Ok(result);
            //if (id != cash.CashCode)
            //{
            //    return BadRequest();
            //}

            //_context.Entry(cash).State = EntityState.Modified;

            //try
            //{
            //    await _context.SaveChangesAsync();
            //}
            //catch (DbUpdateConcurrencyException)
            //{
            //    if (!CashExists(groupId))
            //    {
            //        return NotFound();
            //    }
            //    else
            //    {
            //        throw;
            //    }
            //}

            //return NoContent();
        }

        // POST: api/Cashes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Cash>> PostCash(int groupCode)
        {
            var res = await _cashesDbStore.createCash(groupCode);
            if (res == false)
            {
                return BadRequest();
            }
            return Ok(res);
        }

        // DELETE: api/Cashes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCash(int id)
        {
            if (_context.Cashes == null)
            {
                return NotFound();
            }
            var cash = await _context.Cashes.FindAsync(id);
            if (cash == null)
            {
                return NotFound();
            }

            _context.Cashes.Remove(cash);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CashExists(int id)
        {
            return (_context.Cashes?.Any(e => e.CashCode == id)).GetValueOrDefault();
        }
    }
}
