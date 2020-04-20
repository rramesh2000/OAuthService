using Application.Common.Models;
using AutoMapper;
using Domain.Entities;

namespace Application.Common.Mapper
{
    public class AuthorizeProfile : Profile
    {
        public AuthorizeProfile()
        {
            CreateMap<Authorize, AuthorizeDTO>().ReverseMap();
            CreateMap<AuthResponseDTO, AuthorizeDTO>().ReverseMap();
        }
    }
}
