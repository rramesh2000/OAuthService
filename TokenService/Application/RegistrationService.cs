using Domain;
using FluentValidation.Results;
using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Application
{
    public class RegistrationService:BaseService
    {
        public RegistrationService():base()
        {
            DBService = new DBMSSQLService();
        }

        public RegistrationService(IEncryptionService encryptSvc)
        {
            DBService = new DBMSSQLService();
            EncryptSvc = encryptSvc;
        }

        public IDBService DBService { get; set; }

        public IEncryptionService EncryptSvc { get; set; }

        public Response SaveUser(User user)
        {
            
            try
            {
                ValidationResult results = uservalidation.Validate(user);
                if (!results.IsValid)
                {
                    foreach (var failure in results.Errors)
                    {
                        response.Body += "Property " + failure.PropertyName + " failed validation. Error was: " + failure.ErrorMessage;
                        response.httpstatus = HttpStatusCode.PreconditionFailed;
                    }
                }
                else { 
                    Users _user = mapper.Map<Users>(user);
                    _user.UserId = Guid.NewGuid();
                    _user.Salt = EncryptSvc.GetSalt();
                    _user.HashPassword = EncryptSvc.GenerateSaltedHash(_user.Salt, user.Password).Hash;

                    if (DBService.SaveUser(_user)!=null)
                    {
                        response.Body +="Success";
                        response.httpstatus = HttpStatusCode.Created;
                    }
                }
            }
            catch (Exception ex)
            {
                
                string tmp = ex.Message + "            " + ex.StackTrace;
                response.Body += tmp;
                response.httpstatus = HttpStatusCode.InternalServerError;
            }
            return response;
        }

    }
}
