using Repository.GeneratedModels;
using Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DATA.UsersData
{
    public interface IUsers
    {
        Task<bool> createUser(UsersDto user);
        Task<User> GetUser(string mail, string pass);
        Task<List<UsersDto>> GetUsersInGroup(int groupId);

    }
}
