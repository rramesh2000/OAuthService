using AutoMapper;
using Domain;
using Domain.Entities;
using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Common.Mapper
{
    public class UserProfile: Profile
    {
        public UserProfile()
        {
            CreateMap<User, Users>();
        }
    }
}
