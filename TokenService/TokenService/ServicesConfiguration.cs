using Application.Authentication;
using Application.Common.Behaviours;
using Application.Common.Interfaces;
using Application.JWT;
using Infrastructure.Logging;
using Infrastructure.Models;
using Microsoft.Extensions.DependencyInjection;

namespace TokenService
{
    public static class ServicesConfiguration
    {
        public static void AddOAuthServices(this IServiceCollection services)
        {
            services.AddScoped<IEncryptionService, EncryptionService>();
            services.AddScoped<ITSLogger, TSLogger>();
            services.AddScoped<ITokenService, JWTToken>();
            services.AddScoped<ITokenService, RefreshToken>();
            services.AddScoped<ITokenServiceDbContext, OAuthContext>();
        }

    }

}
