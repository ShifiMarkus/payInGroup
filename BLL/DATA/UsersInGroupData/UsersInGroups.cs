using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Repository.GeneratedModels;
using Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DATA.UsersInGroupData
{
    public class UsersInGroups : IUsersInGroups
    {
        private readonly MyDBContext _context;
        private readonly IMapper _mapper;

        public UsersInGroups(MyDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<bool> AddMameberInGroup(UsersInGroupDto usersInGroup)
        {
            var user = await _context.Users.Where(x => x.UserMail == usersInGroup.Mail).FirstOrDefaultAsync();
            if (user == null)
            {
                //כתובת המייל לא קיימת במערכת
                return false;
            }
            usersInGroup.UserCode = user.UserCode;
            usersInGroup.UserType = "user";
            var res = await _context.UsersInGroups.Where(x=>x.UserCode == user.UserCode && x.GroupCode == usersInGroup.GroupCode).FirstOrDefaultAsync();
            if (res == null)
            {
                //כתובת המייל אכן קיימת במערכת, והיא עדיין לא משויכת לקבוצה הנוכחית
                //במקרה זה מוסיפים את היוזר הרצוי לקבוצה
               
                var userModel = _mapper.Map<UsersInGroup>(usersInGroup);

                await _context.AddAsync(userModel);
                var isOk = await _context.SaveChangesAsync() >= 0;
                if (isOk)
                { return true; }
                return false;
            }
            //היוזר הרצוי כבר נמצא בקבוצה הנוכחית
            return false;
        }

        public async Task<bool> addUserInGroup(int userId, int groupId)
        {
            var user = await _context.UsersInGroups?.Where(x => x.UserCode == userId && x.GroupCode == groupId).FirstOrDefaultAsync();
            if (user != null)
                return false;

            await _context.AddAsync(new UsersInGroup { GroupCode = groupId, UserCode = userId, UserType = "user"});
            var isOk = await _context.SaveChangesAsync() >= 0;
            if (isOk)
            { return true; }
            return false;
        }

        public async Task<bool> createUserInGroup(UsersInGroupDto userInGroup)
        {
            var userInGroupModel = _mapper.Map<UsersInGroup>(userInGroup);
            await _context.AddAsync(userInGroupModel);
            var isOk = await _context.SaveChangesAsync() >= 0;
            if(isOk)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> createUserInGroup(string LeaderId,int res)
        {
            UsersInGroupDto userInGroup = new UsersInGroupDto();
            userInGroup.GroupCode = res;
            userInGroup.UserType = "manager";
            var code = await _context.Users.Where(x => x.UserId == LeaderId).FirstOrDefaultAsync();
            if(code == null)
            {
                return false;
            }
            userInGroup.UserCode = code.UserCode;
            Boolean isOk = await createUserInGroup(userInGroup);
            if(isOk)
            {
                return true;
            }
            return false;
        }

        public async Task<List<UsersInGroup?>> GetGroupsForUser(int userId)
        {
            var groups = await _context.UsersInGroups?.Where(x => x.UserCode == userId).ToListAsync();
           // var userInGroupList = _mapper.Map<UsersInGroupDto>(groups);

            return groups; 
        }

        public Task<string> GetUserInGroup(int userId, int groupId)
        {
            throw new NotImplementedException();
        }

        public Task<UsersInGroupDto> GetUsersInGroup(int id)
        {
            //UsersInGroupDto[] usersInGroup;
            //var users_in_group = _context.UsersInGroups.Where(x => x.GroupCode == id);
            //usersInGroup = users_in_group.(x => x.UserCode);
            throw new NotImplementedException();
        }

        public async Task<bool> removeUserInGroup(int userId,int groupId)
        {
            var user = await _context.UsersInGroups.Where(x => x.UserCode == userId && x.GroupCode == groupId).FirstOrDefaultAsync();
            //var res = await _context.Remove(user).;
            throw new NotImplementedException();
        }
    }
}
