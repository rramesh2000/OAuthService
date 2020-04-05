using AutoMapper;
using Domain;
using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Application
{
    public class DBMSSQLService : BaseService, IDBService
    {
        public OAuthContext oauth { get; set; }
        private readonly IMapper _mapper;
        public DBMSSQLService(IMapper mapper)
        {
            oauth = new OAuthContext();
            _mapper = mapper;
        }

        public DBMSSQLService() : base()
        {
            oauth = new OAuthContext();
        }

        public Users GetUser(string username)
        {
            if (!String.IsNullOrEmpty(username))
            {
                Users user = oauth.Users.Where(x => x.UserName == username).FirstOrDefault();
                if (username == user.UserName)
                {
                    return user;
                }
            }
            return null;
        }


        public Users SaveUser(Users user)
        {
            Users u = new Users();
            try
            {
                oauth.Users.Add(user);
                oauth.SaveChanges();
                u = user;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return u;
        }
    }
}
