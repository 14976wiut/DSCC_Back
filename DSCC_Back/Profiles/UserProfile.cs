using AutoMapper;
using DSCC_Back.DTO;
using DSCC_Back.Models;

namespace DSCC_Back.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserReadDto>();
            CreateMap<UserCreateDto, User>();
            CreateMap<UserUpdateDto, User>();
        }
    }
}
