using Application.Common.Interfaces;

namespace Application.TokenValidation.Models
{
    public class AuthorizationDTO
    {
        public AuthorizationDTO()
        {
        }

        public AuthorizationDTO(string authorization, bool isValid, string secret, ITokenService jWTTokenService)
        {
            Authorization = authorization;
            IsValid = isValid;
            Secret = secret;
            JWTTokenService = jWTTokenService;
        }

        public string Authorization { get; set; }

        public bool IsValid { get; set; }

        public string Secret { get; set; }

        public ITokenService JWTTokenService { get; set; }
    }
}
