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
        public RefreshTokenValidation refreshvalidation { get; set; }
        public ITokenService JWTTokenService { get; set; }
        public IEncryptionService EncryptSvc { get; set; }
        public UserValidation uservalidation { get; set; }
        public ClientValidation clientvalidation { get; set; }
        public ITokenServiceDbContext oauth { get; set; }

        public BaseService(IConfiguration configuration, ITSLogger log, ITokenService jWTTokenService, ITokenServiceDbContext oauth, IEncryptionService encryptSvc)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<UserProfile>();
                cfg.AddProfile<ClientProfile>();
                cfg.AddProfile<AuthorizeProfile>();
            });

            mapper = config.CreateMapper();
            Log = log;
            userloginvalidation = new UserLoginValidation();
            refreshvalidation = new RefreshTokenValidation();
            uservalidation = new UserValidation();
            clientvalidation = new ClientValidation();
            this.config = configuration;
            this.JWTTokenService = jWTTokenService;
            this.oauth = oauth;
            this.EncryptSvc = encryptSvc;
        }
    }
}
