using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace TokenService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        public IConfiguration Configuration { get; set; }
        public BaseController()
        {
            ConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.AddJsonFile("config.json", true);
            Configuration = configurationBuilder.Build();
        }
    }
}