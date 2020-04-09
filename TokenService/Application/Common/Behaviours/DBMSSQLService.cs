using Application.Common.Interfaces;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Models;
using System;
using System.Linq;

namespace Application.Common.Behaviours
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

        public void UpdateUserRefreshToken(string username, string refresh_token)
        {
            Users user = oauth.Users.Where(x => x.UserName == username).FirstOrDefault();
            user.RefreshToken = refresh_token;
            oauth.Update(user);
            oauth.SaveChanges();
        }
        public Users GetUserFromRefreshToken(string refreshtoken)
        {
            if (!String.IsNullOrEmpty(refreshtoken))
            {
                Users user = oauth.Users.Where(x => x.RefreshToken == refreshtoken).FirstOrDefault();
                return user;
            }
            return null;
        }

        public Users SaveUser(User user)
        {
            Users u = mapper.Map<Users>(user);
            try
            {
                //TODO: Prevent addoing duplicate user with same username
                oauth.Users.Add(u);
                oauth.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return u;
        }
    }
}
