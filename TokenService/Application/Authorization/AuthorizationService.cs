using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;

namespace Application.Authorization
{
    public class AuthorizationService : BaseService, IAuthorizationService
    {
        public AuthorizationService(IConfiguration configuration, ITSLogger log, ITokenService jWTTokenService, ITokenServiceDbContext oauth, IEncryptionService encryptSvc) : base(configuration, log, jWTTokenService, oauth, encryptSvc)
        {

        }

        public AuthResponseDTO GetAuthorization(AuthorizeDTO authorizeDTO)
        {
            AuthResponseDTO authResponseDTO = new AuthResponseDTO();
            Authorize authorize = mapper.Map<Authorize>(authorizeDTO);
            Client client = oauth.Client.SingleOrDefault(x => x.Client_Id == authorizeDTO.Client_Id);
            authorize.Code = JWTTokenService.GenerateRefreshToken();
            oauth.Authorize.Add(authorize);
            oauth.SaveChanges();
            authResponseDTO.Code = authorize.Code;
            authResponseDTO.State = authorizeDTO.redirect_uri;
            authResponseDTO.redirect_uri = authorizeDTO.State;
            return authResponseDTO;
        }
         
    }
}
