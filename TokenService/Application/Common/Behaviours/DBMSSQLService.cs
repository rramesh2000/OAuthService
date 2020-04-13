using Application.Common.Interfaces;
using AutoMapper;
using Domain.Entities;
using System;
using System.Linq;

namespace Application.Common.Behaviours
{
    public class DBMSSQLService : BaseService  
    {
        public ITokenServiceDbContext oauth { get; set; }
        private readonly IMapper _mapper;

        public DBMSSQLService(ITSLogger log, ITokenServiceDbContext oauth, IMapper mapper): base(log)
        {
            this.oauth = oauth;
            _mapper = mapper;
        }

        public User GetUser(string username)
        {
            if (!string.IsNullOrEmpty(username))
            {
                User user = oauth.User.Where(x => x.UserName == username).FirstOrDefault();
                if (username == user.UserName)
                {
                    return user;
                }
            }
            return null;
        }

        public void UpdateUserRefreshToken(string username, string refresh_token)
        {
            User user = oauth.User.Where(x => x.UserName == username).FirstOrDefault();
            user.RefreshToken = refresh_token;
           // oauth.Update(user);
           // oauth.SaveChanges();
        }
        public User GetUserFromRefreshToken(string refreshtoken)
        {
            if (!string.IsNullOrEmpty(refreshtoken))
            {
                User user = oauth.User.Where(x => x.RefreshToken == refreshtoken).FirstOrDefault();
                return user;
            }
            return null;
        }

        public User SaveUser(User user)
        {
            User u = mapper.Map<User>(user);
            try
            {
                if (oauth.User.Where(x => x.UserName == user.UserName).Count() < 1)
                {
                    oauth.User.Add(u);
                 //   oauth.SaveChanges();
                }
                else
                {
                    throw new DuplicateWaitObjectException();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return u;
        }
    }
}
