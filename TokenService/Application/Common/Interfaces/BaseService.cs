using Application.Common.Mapper;
using Application.Common.Validation;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Common.Interfaces
{
    public abstract class BaseService
    {

        public IConfiguration config { get; set; }
        public IServiceCollection services { get; set; }
        public IMapper mapper { get; set; }
        public ITSLogger Log { get; set; }

        public UserLoginValidation userloginvalidation { get; set; }

        public UserValidation uservalidation { get; set; }


        public BaseService(ITSLogger log)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<UserProfile>();
            });

            mapper = config.CreateMapper();

            Log = log;


            userloginvalidation = new UserLoginValidation();

            uservalidation = new UserValidation();


        }
    }
}
