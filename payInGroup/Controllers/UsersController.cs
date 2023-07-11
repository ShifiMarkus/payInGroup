using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repository.GeneratedModels;
using Services.DATA.EmailData;
using Services.DATA.UsersData;
using Services.DTO;

namespace payInGroup.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly MyDBContext _context;
        private readonly IUsers _dbStore;
        private readonly IEmail _email;

        public UsersController(MyDBContext context, IUsers dbStore, GoogleStorageManager googleStorageManager, IEmail email)
        {
            _context = context;
            _dbStore = dbStore;
            _email = email;
        }

        // GET: api/Users
        [HttpGet]
        [Route("/api/create")]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
          if (_context.Users == null)
          {
              return NotFound();
          }
            return await _context.Users.ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("/api/users/{mail}/{pass}")]
        public async Task<ActionResult<User>> GetUser(string mail, string pass)
        {
            var user = await _dbStore.GetUser(mail,pass);
            if (user == null)
            {
                return BadRequest();
            }
            return Ok(user);
        }

        [HttpGet]
        [Route("/api/users/getUsersInGroup/{groupId}")]
        public async Task<ActionResult<IEnumerable<UsersDto>>> GetUsersInGroup(int groupId)
        {
            if (_context.Users == null)
            {
                return NotFound();
            }
            return await _dbStore.GetUsersInGroup(groupId);
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.UserCode)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        //[Route("/api/users")]
        public async Task<ActionResult<User>> PostUser(UsersDto user)
        {

            var res = await _dbStore.createUser(user);
            if(res)
            {
                _email.SendEmail(user.UserMail, user.FirstName);
                return Ok(res);
            }
            return BadRequest();
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            if (_context.Users == null)
            {
                return NotFound();
            }
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return (_context.Users?.Any(e => e.UserCode == id)).GetValueOrDefault();
        }
        
    }
}
