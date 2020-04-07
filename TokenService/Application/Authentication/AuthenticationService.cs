using Application.Authentication.Handlers;
using Application.Common.Behaviours;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using FluentValidation.Results;
using Infrastructure.Models;
using System;

namespace Application
{
    public class AuthenticationService : BaseService, IAuthenticationService
    {
        public ITokenService JWTTokenService { get; set; }
        public IDBService DBService { get; set; }

        public IEncryptionService EncryptSvc { get; set; }
        public string SecretKey { get; set; }

        public AuthenticationService()
        {
        }

        public AuthenticationService(string secretKey)
        {
            EncryptSvc = new EncryptionService();
            SecretKey = secretKey;
            DBService = new DBMSSQLService();
            JWTTokenService = new JWTTokenService(DBService, EncryptSvc, SecretKey);
        }

        public AuthenticationService(IEncryptionService encryptSvc, string secretKey)
        {
            EncryptSvc = encryptSvc;
            SecretKey = secretKey;
            DBService = new DBMSSQLService();
            JWTTokenService = new JWTTokenService(DBService, EncryptSvc, SecretKey);
        }

        public AuthenticationService(IDBService dBService, IEncryptionService encryptSvc, string secretKey)
        {
            DBService = dBService;
            EncryptSvc = encryptSvc;
            SecretKey = secretKey;
        }

        public AuthenticationService(ITokenService jWTTokenService, IDBService dBService, IEncryptionService encryptSvc, string secretKey)
        {
            JWTTokenService = jWTTokenService;
            DBService = dBService;
            EncryptSvc = encryptSvc;
            SecretKey = secretKey;
        }


        public AuthenticationDTO Authenticate(UserLoginDTO userLoginDTO)
        {
            AuthenticationDTO auth = new AuthenticationDTO();
            try
            {
                UserLogin userLogin = mapper.Map<UserLogin>(userLoginDTO);
                ValidationResult results = userloginvalidation.Validate(userLogin);                
                userLoginDTO.users = mapper.Map<Users>(DBService.GetUser(userLogin.username));
                userLoginDTO.encryptionService = new EncryptionService();
                               
                var handler = new UserAuthenticationHandler();
                handler.Handle(userLoginDTO);
                auth.token_type = "bearer";
                auth.access_token = JWTTokenService.GetToken(userLoginDTO.users);                 
                auth.refresh_token = JWTTokenService.GetToken(userLoginDTO.users);
            }
            catch(Exception ex)
            {
                Log.Log.Error(ex.Message, "Invalid User");
                throw new InvalidUserException("Invalid User");
            }

            return auth;
        }
    }
}
