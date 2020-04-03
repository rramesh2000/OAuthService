using AutoMapper;
using Domain;
using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application
{
    public class UserProfile: Profile
    {
        public UserProfile()
        {
            CreateMap<User, Users>();
        }
    }
}
