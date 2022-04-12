using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Examen.Models.Data;

namespace Examen.Middleware
{
    public static class Connection
    {
        public static IServiceCollection AddConnection(this IServiceCollection services, IConfiguration _Configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                  options.UseSqlServer(_Configuration.GetConnectionString("DefaultConnection"), o => o.CommandTimeout(180))
                  .EnableSensitiveDataLogging(true).UseLoggerFactory(MyLoggerFactory));
            return services;
        }
        public static readonly ILoggerFactory MyLoggerFactory = LoggerFactory.Create(builder =>
        {
            builder.AddFilter((category, level) =>
                category == DbLoggerCategory.Database.Command.Name
                && level == LogLevel.Information);
        });
    }
}
