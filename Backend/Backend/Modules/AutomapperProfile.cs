using AutoMapper;
using Backend.Common.Dtos;
using Backend.DAL.Models;

namespace Backend.Modules
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<User, UserInfoDto>();
        }
    }
}