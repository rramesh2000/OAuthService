
using Application.Common.Models;
using AutoMapper;

namespace Application.Common.Mapper
{
    public class TokenProfile : Profile
    {
        public TokenProfile()
        {
            CreateMap<TokenDTO, AuthorizationGrantRequestDTO>().ReverseMap();
        }
    }
}


