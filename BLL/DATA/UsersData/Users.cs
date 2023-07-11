using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Repository.GeneratedModels;
using Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DATA.UsersData
{
    public class Users : IUsers
    {
        private readonly MyDBContext _context;
        private readonly IMapper _mapper;
        public Users(MyDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<bool> createUser(UsersDto user)
        {
            var UserId = await _context.Users.Where(x => x.UserId == user.UserId).FirstOrDefaultAsync();
            if (UserId != null)
                return false;
            var userModel = _mapper.Map<User>(user);

            await _context.AddAsync(userModel);
            try
            {

           var isOk= await _context.SaveChangesAsync()>=0;
            if(isOk)
            { return true; }
            }catch(Exception ex)
            {

            }
            return false;
        }

        public async Task<User> GetUser(string mail, string pass)
        {
            var emailFromData = await _context.Users.FirstOrDefaultAsync(x => x.UserMail == mail);
            if (emailFromData != null)
            {
                var passWordFromData = await _context.Users.FirstOrDefaultAsync(x => x.UserPassword == pass&&x.UserMail== emailFromData.UserMail);
                if (passWordFromData != null)
                {
                    return passWordFromData;
                }
                else
                {
                    //return "errorPassword";
                    return null;
                }
            }
            else
            {
                //return "notFoundEmail";
                return null;
            }
            //var user = await _context.Users.Where(u => u.UserMail == mail && u.UserPassword == pass).FirstOrDefaultAsync();
            //return user != null;
        }

        public async Task<List<UsersDto>> GetUsersInGroup(int groupId)
        {
            var codesList = await _context.UsersInGroups
    .Where(x => x.GroupCode == groupId)
    .Select(x => new { x.UserCode, x.UserType })
    .ToListAsync();
            //var codesList = await _context.UsersInGroups.Where(x => x.GroupCode == groupId).Select(x=>x.UserCode).ToListAsync();
            if (codesList.Count > 0)
            {
                var userCodes = codesList.Select(x => x.UserCode).ToList();
                var usersList = await _context.Users
                    .Where(x => userCodes.Contains(x.UserCode))
                    .ToListAsync();
                var mergedList =  codesList.Join(usersList,
        code => code.UserCode,
        user => user.UserCode,
        (code, user) => new UsersDto{ UserType =  code.UserType,FirstName = user.FirstName, LastName = user.LastName,
                                      UserId = user.UserId, UserMail = user.UserMail, UserPassword = user.UserPassword,
                                      UserPhone = user.UserPhone}).ToList();
                // var res = await _mapper.Map<UsersDto>(mergedList);
                return mergedList;
            }
            return null;
        }
    }
}
