using Application.Common.Models;
using AutoMapper;
using Domain.Entities;

namespace Application.Common.Mapper
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<UserDTO, User>().ReverseMap();
        }
    }
}
