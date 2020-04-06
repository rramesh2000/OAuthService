using Application.Common.Models;
using AutoMapper;
using Domain;
using Domain.Entities;
using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Common.Mapper
{
    public class UserLoginProfile: Profile
    {
        public UserLoginProfile()
        {
            CreateMap<UserLogin,UserLoginDTO>();
        }
    }
}
