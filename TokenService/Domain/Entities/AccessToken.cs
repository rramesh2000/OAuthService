using Domain.Common;

namespace Domain.Entities
{
    public class AccessToken : AuditableEntity
    {
        public AccessToken()
        {
        }

        public AccessToken(string authorization)
        {
            Authorization = authorization;
        }

        public string Authorization { get; set; }
    }
}
