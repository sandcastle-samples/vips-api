using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;

namespace VipsApi.Tests.Configuration
{
    public class TestApplicationFactory : WebApplicationFactory<Startup>
    {
        public readonly IConfiguration Configuration;

        public TestApplicationFactory()
        {
            Configuration = BuildConfiguration();
        }

        static IConfiguration BuildConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile("appsettings.CI.json", true, true)
                .AddJsonFile("appsettings.Development.json", true, true)
                .AddEnvironmentVariables();

            return builder.Build();
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureAppConfiguration(cfg =>
            {
                cfg.Sources.Clear();
                cfg.AddConfiguration(Configuration);
            });
        }
    }
}
