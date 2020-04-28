using Application.Authentication.Handlers;
using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using FluentValidation.Results;
using Microsoft.Extensions.Configuration;
using System.Linq;

namespace Application.Authentication
{

    public class UserLogin : BaseService
    {
        public UserLogin(ITokenService refreshtoken, IConfiguration configuration, ITSLogger log, ITokenService jWTTokenService, ITokenServiceDbContext oauth, IEncryptionService encryptSvc) : base(refreshtoken, configuration, log, jWTTokenService, oauth, encryptSvc)
        {

        }
        public UserDTO Login(UserDTO userLogin)
        {
            ValidationResult results = userloginvalidation.Validate(userLogin);
            User user = oauth.User.Where(x => x.UserName == userLogin.UserName).FirstOrDefault();
            UserDTO userLoginDTO = mapper.Map<UserDTO>(user);
            userLoginDTO.password = userLogin.password;
            var handler = new UserAuthenticationHandler();
            handler.Handle(userLoginDTO);
            userLoginDTO.IsAuthenticated = true;
            return userLoginDTO;
        }
    }
}
