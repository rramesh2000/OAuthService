using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application
{
    public class BaseService
    {
        public IServiceCollection services { get; set; }
        public IMapper mapper { get; set; }
        public BaseService()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<UserProfile>();
            });

            mapper = config.CreateMapper();
        }
    }
}
