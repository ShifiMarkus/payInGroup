using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repository.GeneratedModels;
using Services.DATA.EmailData;
using Services.DATA.UsersData;
using Services.DATA.UsersInGroupData;
using Services.DTO;

namespace payInGroup.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersInGroupsController : ControllerBase
    {
        private readonly MyDBContext _context;
        private readonly IUsersInGroups _DbStore;
        private readonly IUsers _DbStoreUsers;
        private readonly IEmail _email;

        public UsersInGroupsController(MyDBContext context, IUsersInGroups usersInGroups, IUsers users, IEmail email)
        {
            _context = context;
            _DbStore = usersInGroups;
            _DbStoreUsers = users;
            _email = email;
        }

        // GET: api/UsersInGroups
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsersInGroup>>> GetUsersInGroups()
        {
          if (_context.UsersInGroups == null)
          {
              return NotFound();
          }
            return await _context.UsersInGroups.ToListAsync();
        }

        // GET: api/UsersInGroups/5
        [HttpGet("{userId}/{groupId}")]
        public async Task<ActionResult<UsersInGroup>> GetUsersInGroup(int userId, int groupCode)
        {
            return null;
            //var userInGroup = await _DbStore.
          //if (_context.UsersInGroups == null)
          //{
          //    return NotFound();
          //}
          //  var usersInGroup = await _context.UsersInGroups.FindAsync(id);

            //  if (usersInGroup == null)
            //  {
            //      return NotFound();
            //  }

            //  return usersInGroup;
        }

        // PUT: api/UsersInGroups/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsersInGroup(int id, UsersInGroup usersInGroup)
        {
            if (id != usersInGroup.GroupCode)
            {
                return BadRequest();
            }

            _context.Entry(usersInGroup).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsersInGroupExists(id))
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

        [HttpPost]
        [Route("/api/UsersInGroups/addMemberInGroup/")]
        public async Task<ActionResult<User>> AddMemberInGroup(UsersInGroupDto userInGroup)
        {
            var res = await _DbStore.AddMameberInGroup(userInGroup);
            if (res == false)
            {
                return BadRequest();
            }
            var users =await _DbStoreUsers.GetUsersInGroup(userInGroup.GroupCode);
            if(users == null)
            {
                return BadRequest();
            }
            var user = users.Where(x=>x.UserMail == userInGroup.Mail)?.FirstOrDefault();
            var group = _context.Groups.Where(x=> x.GroupCode == userInGroup.GroupCode).FirstOrDefault();
            _email.sendAddMemberToGroup(user.UserMail,user.FirstName + " " + user.LastName,group.GroupDescription);
            return Ok(users);
        }
       
        // POST: api/UsersInGroups
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UsersInGroup>> PostUsersInGroup(UsersInGroup usersInGroup)
        {
          if (_context.UsersInGroups == null)
          {
              return Problem("Entity set 'MyDBContext.UsersInGroups'  is null.");
          }
            _context.UsersInGroups.Add(usersInGroup);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (UsersInGroupExists(usersInGroup.GroupCode))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetUsersInGroup", new { id = usersInGroup.GroupCode }, usersInGroup);
        }

        // DELETE: api/UsersInGroups/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsersInGroup(int id)
        {
            if (_context.UsersInGroups == null)
            {
                return NotFound();
            }
            var usersInGroup = await _context.UsersInGroups.FindAsync(id);
            if (usersInGroup == null)
            {
                return NotFound();
            }

            _context.UsersInGroups.Remove(usersInGroup);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UsersInGroupExists(int id)
        {
            return (_context.UsersInGroups?.Any(e => e.GroupCode == id)).GetValueOrDefault();
        }
    }
}
