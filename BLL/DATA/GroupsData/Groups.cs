using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Repository.GeneratedModels;
using Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DATA.GroupsData
{
    public class Groups : IGroups
    {
        private readonly MyDBContext _context;
        private readonly IMapper _mapper;

        public Groups(MyDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> createGroup(GroupsDto group)
        {
            var groupModel = _mapper.Map<Group>(group);
            await _context.AddAsync(groupModel);
            var isOk = await _context.SaveChangesAsync() >= 0;

            if (isOk)
            {
                return groupModel.GroupCode;
            }
            return -1;
        }

        public async Task<List<GroupsDetailsDto>> getGroupsByUserID(int userID)
        {

            var res = await (from g in _context.Groups
                            join u in _context.UsersInGroups
                            on g.GroupCode equals u.GroupCode
                            where u.UserCode == userID
                            select new GroupsDetailsDto
                            {
                                GroupCode = g.GroupCode,
                                GroupDescription = g.GroupDescription,
                                GroupType = g.GroupType,
                                membersInGroup = _context.UsersInGroups.Where(x=>x.GroupCode==g.GroupCode).Count(),
                            }).ToListAsync();
            //var x = res.DistinctBy(x => x.GroupCode).ToList();

            return res;
        }

        //public async Task<IOrderedQueryable> GetGroupsByManagerId(int managerId)
        //{
        //    //var res = await _context.UsersInGroups.Where(x => x.UserCode == managerId && x.UserType == "manager").();
        //    //List<GroupsDto> = _context.Groups.Where(x => x.) 
        //    try
        //    {

        //        var res = await (from g in _context.Groups
        //                         join u in _context.UsersInGroups
        //                         on g.GroupCode equals u.GroupCode

        //                         where u.UserCode == managerId

        //                         select new GroupsDetailsDto
        //                         {
        //                            GroupDescription = g.GroupDescription,
        //                            GroupType = g.GroupType,
        //                            membersInGroup = _context.UsersInGroups.Where(x=>x.GroupCode == g.GroupCode).Count()
        //                         }).OrderByDescending();
        //        return res;
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //    return 1;
        //}
    }
}
