using log4net;
using log4net.Config;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using VTS.QLNS.CTC.Core;

namespace VTS.QLNS.CTC.App.DIConfigure
{
    public static class AddConfigurationDIConfigureExtensions
    {
        public static IServiceCollection AddConfiguration(this IServiceCollection services)
        {
            // Configuration
            var configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("AppData/_configs/appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile("AppData/_configs/dbconfig.json", optional: false, reloadOnChange: true);

            // Log4net
            XmlConfigurator.Configure(new FileInfo("AppData/_configs/log4net.config"));
            services.AddSingleton(LogManager.GetLogger(typeof(object)));
            services.AddLogging();

            var configuration = configurationBuilder.Build();
            services.AddSingleton<IConfiguration>(c => configuration);

            string connectionType = configuration.GetSection("DbSettings:ConnectionType").Value;
            string connectionString = configuration.GetConnectionString(connectionType);
            Action<DbContextOptionsBuilder> configureDbContext = options =>
            {
                options.UseSqlServer(connectionString, sqlServerOptions => sqlServerOptions.CommandTimeout(6000));
                options.EnableSensitiveDataLogging();
                options.ConfigureWarnings(x => x.Ignore(RelationalEventId.AmbientTransactionWarning));
            };
            services.AddDbContext<ApplicationDbContext>(configureDbContext);
            services.AddSingleton(new ApplicationDbContextFactory(configureDbContext));
            return services;
        }
    }
}
