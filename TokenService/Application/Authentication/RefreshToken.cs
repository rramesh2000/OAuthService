using Application.Common.Interfaces;
using Domain.Entities;
using Microsoft.Extensions.Configuration;
using System.Linq;

namespace Application.Authentication
{
    public class RefreshToken : BaseService
    {

        public RefreshToken(IConfiguration configuration, ITSLogger log, ITokenService jWTTokenService, ITokenServiceDbContext oauth, IEncryptionService encryptSvc) : base(configuration, log, jWTTokenService, oauth, encryptSvc)
        {
        }
        public  string GetRefreshToken(string Code)
        {
            string refresh_token = JWTTokenService.GenerateRefreshToken();
            Authorize authorize = oauth.Authorize.SingleOrDefault(x => x.Code == Code);
            authorize.Code = refresh_token;
            oauth.SaveChanges();
            return refresh_token;
        }
    }
}
