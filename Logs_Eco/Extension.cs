using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logs_Eco
{
    public static class Extension
    {
        private static readonly string LogSectionName = "configuracionLog";
        public static IServiceCollection AddLogger(this IServiceCollection services, string user = "")
        {
            IConfiguration _configuration;
            using (var serviceProvider = services.BuildServiceProvider())
            {
                _configuration = serviceProvider.GetService<IConfiguration>();
            }
            LoggerOptions _options = new LoggerOptions();
            _configuration.GetSection(LogSectionName).Bind(_options);
            services.AddSingleton<ILogger, Logger>(sp =>
            {
                return new Logger(_options.rutaArchivoLog, _options.nivel, user);
            });
            return services;
        }
    }
}
