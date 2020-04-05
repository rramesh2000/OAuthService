using AutoMapper; 
using Microsoft.Extensions.DependencyInjection;
using Infrastructure.Logging;
using Application.Validation;
using Domain;

namespace Application
{
    public class BaseService
    {
        public IServiceCollection services { get; set; }
        public IMapper mapper { get; set; }
        public TSLogger Log { get; set; }

        public UserLoginValidation userloginvalidation { get; set; }

        public UserValidation uservalidation { get; set; }

        public Response response { get; set; }
        public BaseService()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<UserProfile>();
            });

            mapper = config.CreateMapper();

            Log = new TSLogger();

            userloginvalidation = new UserLoginValidation();

            uservalidation = new UserValidation();
                       
            response = new Response();
        }
    }
}
