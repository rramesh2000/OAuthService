using Application.Common.Behaviours;
using Application.Common.Interfaces;
using Infrastructure.Logging;
using Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace TokenService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        public IConfiguration configuration;
        public ITSLogger itsLogger { get; set; }
        public ITokenService JWTTokenService { get; set; }
        public ITokenServiceDbContext OAuthDbContext { get; set; }
        public IEncryptionService EncryptionService { get; set; }
        public BaseController(IConfiguration configuration)
        {
            this.configuration = configuration;            
            EncryptionService = new EncryptionService();
            itsLogger = new TSLogger();
            OAuthDbContext = new OAuthContext();
            JWTTokenService = new JWTTokenService(itsLogger, EncryptionService, this.configuration);
        }
    }
}