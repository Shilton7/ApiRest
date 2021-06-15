using Elmah.Io.AspNetCore;
using Elmah.Io.Extensions.Logging;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevIO.Api.Configuration
{
    public static class LoggerConfig
    {
        public static IServiceCollection AddLoggingConfiguration(this IServiceCollection services,
                                                                IConfiguration _configuration)
        {  // Logs de exception, 404  etc
            services.AddElmahIo(o =>
            {
                o.ApiKey = _configuration.GetSection("Logger").GetSection("ApiKey").Value;
                o.LogId = new Guid(_configuration.GetSection("Logger").GetSection("LogId").Value);
            });

            // Setando o Elmah como provider de logger para capturar os logs manuais criados
            /*
            services.AddLogging(builder =>
            {
                builder.AddElmahIo(o =>
                {
                    o.ApiKey = _configuration.GetSection("Logger").GetSection("ApiKey").Value;
                    o.LogId = new Guid(_configuration.GetSection("Logger").GetSection("LogId").Value);
                });
                // Do nivel Warning para baixo
                builder.AddFilter<ElmahIoLoggerProvider>(null, LogLevel.Warning);
            });
            */

            return services;
        }

        public static IApplicationBuilder UseLoggingConfiguration(this IApplicationBuilder app)
        {
            app.UseElmahIo();

            return app;
        }

    }
}
