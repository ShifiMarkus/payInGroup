using Repository.GeneratedModels;
using Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DATA.UsersInGroupData
{
    public interface IUsersInGroups
    {
        Task<bool> createUserInGroup(UsersInGroupDto userInGroup);
        Task<bool> createUserInGroup(string LeaderID,int res);
        Task<UsersInGroupDto> GetUsersInGroup(int id);

        //this function return all the group for user
        Task<List<UsersInGroup?>> GetGroupsForUser(int id);
        Task<string> GetUserInGroup(int userId, int groupId);
        Task<bool> addUserInGroup(int userId, int groupId);
        Task<bool> removeUserInGroup(int userId, int groupId);
        Task<bool> AddMameberInGroup(UsersInGroupDto userInGroup);
    }
}
