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
        public ITokenService JWTTokenService { get; set; }
        public ITokenServiceDbContext OAuthDbContext { get; set; }
        public IEncryptionService EncryptionService { get; set; }
        public BasePage(IConfiguration configuration)
        {
            this.configuration = configuration;
            EncryptionService = new EncryptionService();
            itsLogger = new TSLogger();
            var optionsBuilder = new DbContextOptionsBuilder<OAuthContext>();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("OAuthDatabase"));
            OAuthDbContext = new OAuthContext(optionsBuilder.Options);
            JWTTokenService = new JWTTokenService(itsLogger, EncryptionService, this.configuration);
        }
    }
}
