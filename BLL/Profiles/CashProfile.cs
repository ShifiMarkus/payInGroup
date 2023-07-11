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
    public class CashProfile : Profile
    {
        public CashProfile()
        {
            CreateMap<Cash, CashDto>();
            CreateMap<Cash, CashDetailesDto>();
            CreateMap<CashDetailesDto,Cash > ();
            CreateMap<CashDto, Cash>();
        }
    }
}
