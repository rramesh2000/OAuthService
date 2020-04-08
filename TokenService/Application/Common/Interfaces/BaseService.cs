using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Infrastructure.Logging;
using Domain;
using Application.Common.Validation;
using Application.Common.Mapper;
using Domain.Common;
using Microsoft.Extensions.Configuration;

namespace Application.Common.Interfaces
{
    public abstract class BaseService
    {

        public IConfiguration config { get; set; }
        public IServiceCollection services { get; set; }
        public IMapper mapper { get; set; }
        public TSLogger Log { get; set; }

        public UserLoginValidation userloginvalidation { get; set; }

        public UserValidation uservalidation { get; set; }

       
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
                       
          
        }
    }
}
