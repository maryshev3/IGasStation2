using IGasStation2.EntityFrameworkContexts;
using IGasStation2.Views;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGasStation2.ViewModels
{
    public class ViewModelLocator
    {
        private static IConfiguration _configuration;

        private static ServiceProvider _serviceProvider;

        public ShowDbVM ShowDbVM => _serviceProvider.GetRequiredService<ShowDbVM>();

        private static void InitConfiguration(IServiceCollection services)
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile(
                    "appsettings.json",
                    optional: true,
                    reloadOnChange: true
                );

            _configuration = builder.Build();
        }

        private static void AddDbContext(IServiceCollection services)
        {
            string connectionString = _configuration["ConnectionString:PostgreSQL"];

            var optionsBuilder = new DbContextOptionsBuilder<GasStationContext>();

            var options = optionsBuilder
                .UseNpgsql(connectionString)
                .Options;

            services.AddSingleton(_ => new GasStationContext(options));
        }

        public static void Init()
        {
            var services = new ServiceCollection();

            InitConfiguration(services);

            AddDbContext(services);

            services.AddSingleton<ShowDbVM>();

            _serviceProvider = services.BuildServiceProvider();
        }
    }
}
