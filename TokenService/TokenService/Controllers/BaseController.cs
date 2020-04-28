using Application.Authentication;
using Application.Common.Behaviours;
using Application.Common.Interfaces;
using Application.JWT;
using Infrastructure.Logging;
using Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace TokenService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        public IConfiguration configuration;
        public ITSLogger itsLogger { get; set; }
        public ITokenService RefreshToken { get; set; }
        public ITokenService JWTToken { get; set; }
        public ITokenServiceDbContext OAuthDbContext { get; set; }
        public IEncryptionService EncryptionService { get; set; }
        public BaseController(IConfiguration configuration)
        {
            this.configuration = configuration;
            EncryptionService = new EncryptionService();
            itsLogger = new TSLogger();
            var optionsBuilder = new DbContextOptionsBuilder<OAuthContext>();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("OAuthDatabase"));
            OAuthDbContext = new OAuthContext(optionsBuilder.Options);
            JWTToken = new JWTToken(itsLogger, EncryptionService, this.configuration);
            RefreshToken = new RefreshToken();
        }
    }
}