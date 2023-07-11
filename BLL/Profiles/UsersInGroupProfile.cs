using AutoMapper;
using Repository.GeneratedModels;
using Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Profiles
{
    public class UsersInGroupProfile : Profile
    {
        public UsersInGroupProfile()
        {
            CreateMap<UsersInGroup, UsersInGroupDto>();
            CreateMap<UsersInGroupDto, UsersInGroup>();
        }
    }
}
