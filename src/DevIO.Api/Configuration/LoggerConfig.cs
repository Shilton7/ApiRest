using System;
using DevIO.Api.Extensions;
using Elmah.Io.AspNetCore;
using Elmah.Io.AspNetCore.HealthChecks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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

            services.AddHealthChecksUI()
                .AddSqlServerStorage(_configuration.GetConnectionString("DefaultConnection"));

            services.AddHealthChecks()
                    .AddElmahIoPublisher(options =>
                    {
                        options.ApiKey = _configuration.GetSection("Logger").GetSection("ApiKey").Value;
                        options.LogId = new Guid(_configuration.GetSection("Logger").GetSection("LogId").Value);
                        options.HeartbeatId =  "Api Rest Shilton";
                    })

                    .AddSqlServer(_configuration.GetConnectionString("DefaultConnection"), name: "HealthCheck-BancoSQLServer")
                    .AddCheck("Table Produtos", new SqlServerHealthCheck(_configuration.GetConnectionString("DefaultConnection")));

            return services;
        }

        public static IApplicationBuilder UseLoggingConfiguration(this IApplicationBuilder app)
        {
            app.UseElmahIo();

            return app;
        }

    }
}
