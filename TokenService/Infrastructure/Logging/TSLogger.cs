using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Core;

namespace Infrastructure.Logging
{
    public class TSLogger
    {
        public Logger Log { get; set; }

        public TSLogger()
        {
            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

            Log = new LoggerConfiguration().ReadFrom.Configuration(configuration).CreateLogger();
            
        }
    }
}
