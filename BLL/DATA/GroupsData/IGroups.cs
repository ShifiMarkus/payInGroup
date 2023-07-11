using Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DATA.GroupsData
{
    public interface IGroups
    {
        Task<int> createGroup(GroupsDto group);
        Task<List<GroupsDetailsDto>> getGroupsByUserID(int userID);
    }
}
