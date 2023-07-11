using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repository.GeneratedModels;
using Services.DATA.CashData;
using Services.DATA.GroupsData;
using Services.DATA.UsersInGroupData;
using Services.DTO;

namespace payInGroup.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupsController : ControllerBase
    {
        private readonly MyDBContext _context;
        private readonly IGroups _dbStore;
        private readonly IUsersInGroups _userInGroup;
        private readonly ICashes _cash;


        public GroupsController(MyDBContext context, IGroups dbStore, IUsersInGroups userInGroup, ICashes cash)
        {
            _context = context;
            _dbStore = dbStore;
            _userInGroup = userInGroup;
            _cash = cash;
        }

        // GET: api/Groups
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Group>>> GetGroups()
        {
            if (_context.Groups == null)
            {
                return NotFound();
            }
            return await _context.Groups.ToListAsync();
        }

        // GET: api/Groups/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Group>> GetGroup(int id)
        {
            //get user code 
            //return count(id) where users_in_group.user_code == id
            if (_context.Groups == null)
            {
                return NotFound();
            }
            return NotFound();
        }

        [HttpGet]
        [Route("/api/Groups/getGroupsByUserID/{userID}")]
        public async Task<ActionResult<Group>> getGroupsByUserID(int userID)
        {
            var groupsList = await _dbStore.getGroupsByUserID(userID);
            if(groupsList == null)
            {
                return BadRequest();
            }
            return Ok(groupsList);

        }
        // PUT: api/Groups/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGroup(int id, Group @group)
        {
            if (id != @group.GroupCode)
            {
                return BadRequest();
            }

            _context.Entry(@group).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GroupExists(id))
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

        // POST: api/Groups
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Group>> PostGroup(GroupsDto group)
        {
            var res = await _dbStore.createGroup(group);    
            if (res != -1)
            {
                var x = await _userInGroup.createUserInGroup(group.LeaderID, res);
                if (x!=null)
                {
                    //var ans = await _cash.createCash(res);
                    //if(ans != null)
                        return Ok();
                }
            }
            return BadRequest();
        }

        [HttpPost]
        [Route("/api/Groups/createNewGroupAndCash")]
        public async Task<ActionResult<bool>> CreateNewGroupAndCash(GroupAndCashDto groupAndCashDto)
        {

            var res = await _dbStore.createGroup(groupAndCashDto.groupDetails);
            if (res != -1)
            {
                var x = await _userInGroup.createUserInGroup(groupAndCashDto.groupDetails.LeaderID, res);
                if (x != null)
                {
                   
                    var ans = await _cash.createCash(res, groupAndCashDto.cashDetails);
                    if(ans != null)
                    {
                        return Ok(ans);
                    }
                   
                }
            }
            return BadRequest();
        }

        // DELETE: api/Groups/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGroup(int id)
        {
            if (_context.Groups == null)
            {
                return NotFound();
            }
            var @group = await _context.Groups.FindAsync(id);
            if (@group == null)
            {
                return NotFound();
            }

            _context.Groups.Remove(@group);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GroupExists(int id)
        {
            return (_context.Groups?.Any(e => e.GroupCode == id)).GetValueOrDefault();
        }
    }
}
