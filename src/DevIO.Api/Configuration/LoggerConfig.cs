using DevIO.Api.Extensions;
using Elmah.Io.AspNetCore;
using Elmah.Io.AspNetCore.HealthChecks;
using Elmah.Io.Extensions.Logging;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
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

            services.AddHealthChecksUI();

            services.AddHealthChecks()
                    .AddElmahIoPublisher(_configuration.GetSection("Logger").GetSection("ApiKey").Value, 
                                new Guid(_configuration.GetSection("Logger").GetSection("LogId").Value),"Api Rest Shilton")

                    .AddSqlServer(_configuration.GetConnectionString("DefaultConnection"), name: "HealthCheck-BancoSQLServer")
                    .AddCheck("Table Produtos", new SqlServerHealthCheck(_configuration.GetConnectionString("DefaultConnection")));

            return services;
        }

        public static IApplicationBuilder UseLoggingConfiguration(this IApplicationBuilder app)
        {
            app.UseElmahIo();

            app.UseHealthChecksUI(options => { options.UIPath = "/api/hc-ui"; });
            app.UseHealthChecks("/api/hc", new HealthCheckOptions()
            {
                Predicate = _ => true,
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });

            return app;
        }

    }
}
