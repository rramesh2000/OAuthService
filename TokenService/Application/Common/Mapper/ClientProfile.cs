using Application.Common.Models;
using AutoMapper;
using Domain.Entities;

namespace Application.Common.Mapper
{
    public class ClientProfile: Profile
    {
        public ClientProfile()
        {
            CreateMap<Client, ClientDTO>().ReverseMap();
        }
    }
}
