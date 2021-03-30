using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SeleniumNunitTestExample.Settings;
using System;

namespace SeleniumNunitTestExample
{
    public class Startup
    {
        private readonly IConfiguration _appConfiguration;
        public Startup(IConfiguration appConfiguration)
        {
            _appConfiguration = appConfiguration;
            var builder = new ConfigurationBuilder()
                .SetBasePath(Environment.CurrentDirectory)
               .AddJsonFile("appsettings.json", optional: true);
            _appConfiguration = builder.Build();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<ConnectionStrings>(_appConfiguration.GetSection("ConnectionStrings"));
        }
    }
}
