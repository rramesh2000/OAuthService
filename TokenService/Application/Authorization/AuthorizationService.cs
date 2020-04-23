using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using Domain.Enums;
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

        public AuthorizationResponseDTO GetAuthorization(AuthorizationRequestDTO authorizeDTO)
        {
            AuthorizationResponseDTO authResponseDTO = new AuthorizationResponseDTO();
            try
            {              
                Authorize authorize = mapper.Map<Authorize>(authorizeDTO);
                Client client = oauth.Client.SingleOrDefault(x => x.Client_Id == authorizeDTO.Client_Id);
                if (client.Client_Id != authorizeDTO.Client_Id) { throw new InvalidClientException(TokenConstants.InvalidClient); }
                authorize.Code = JWTTokenService.GenerateRefreshToken();
                oauth.Authorize.Add(authorize);
                oauth.SaveChanges();
                authResponseDTO.Code = authorize.Code;
                authResponseDTO.State = authorizeDTO.State;
                authResponseDTO.Redirect_Uri = authorizeDTO.Redirect_Uri;
            }
            catch {
                
            }
            return authResponseDTO;
        }
         
    }
}
