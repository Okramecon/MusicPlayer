using AutoMapper;
using MusicPlayer.DAL.Entities;
using MusicPlayer.Models.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicPlayer.API
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Account, AccountResponse>()
                .ForMember("RoleName", opt => opt.MapFrom(c => c.Role.Name)).ReverseMap();
        }
    }
}
