using Domain;
using Infrastructure.Models;
using System;
using System.Collections.Generic;
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

        public bool SaveUser(User user)
        {
            bool tmpBool = false;
            try
            {
                Users _user = mapper.Map<Users>(user);
                _user.UserId = Guid.NewGuid();
                _user.Salt = EncryptSvc.GetSalt();
                _user.HashPassword = EncryptSvc.GenerateSaltedHash(_user.Salt, user.Password).Hash;
                
                if (DBService.SaveUser(_user))
                {
                    tmpBool = true;
                }
            }
            catch (Exception ex)
            {
                string tmp = ex.Message + "            " + ex.StackTrace;
                tmpBool = false;
            }
            return tmpBool;
        }

    }
}
