using Application.Authentication;
using Application.Common.Behaviours;
using Application.Common.Interfaces;
using Application.JWT;
using Infrastructure.Logging;
using Infrastructure.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace TokenService.Pages
{
    public class BasePage : PageModel
    {
        public IConfiguration configuration;
        public ITSLogger itsLogger { get; set; }
        public ITokenService jwtToken { get; set; }
        public ITokenService refToken { get; set; }
        public ITokenServiceDbContext OAuthDbContext { get; set; }
        public IEncryptionService EncryptionService { get; set; }
        public BasePage(IConfiguration configuration)
        {
            this.configuration = configuration;
            EncryptionService = new EncryptionService();
            refToken = new RefreshToken();
            itsLogger = new TSLogger();
            var optionsBuilder = new DbContextOptionsBuilder<OAuthContext>();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("OAuthDatabase"));
            OAuthDbContext = new OAuthContext(optionsBuilder.Options);
            jwtToken = new JWTToken(itsLogger, EncryptionService, this.configuration);
        }
    }
}
