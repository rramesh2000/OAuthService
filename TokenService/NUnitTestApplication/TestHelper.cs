using Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;

namespace NUnitTestApplication
{
    public class TestHelper
    {
        public static IConfigurationRoot GetIConfigurationRoot(string outputPath)
        {
            return new ConfigurationBuilder()
                .SetBasePath(outputPath)
                .AddJsonFile("appsettings.json", optional: true)
                .AddEnvironmentVariables()
                .Build();
        }

        public static ITokenServiceDbContext GetTokenServiceDbContext()
        {
            var options = new DbContextOptionsBuilder<Infrastructure.Models.OAuthContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            var context = new Infrastructure.Models.OAuthContext(options);
            context.Database.EnsureCreated();
            return context;
        }
    }
}
