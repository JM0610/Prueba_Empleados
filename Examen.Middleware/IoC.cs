using Examen.IServices;
using Microsoft.Extensions.DependencyInjection;
using Examen.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Examen.Middleware
{
    public static class IoC
    {
        public static IServiceCollection AddDependency(this IServiceCollection services)
        {
            services.AddTransient<IArea, AreaServices>();
            services.AddTransient<IEmpleado, EmpleadoServices>();
            services.AddTransient<IHabilidad, HabilidadServices>();
            return services;
        }
    }
}
