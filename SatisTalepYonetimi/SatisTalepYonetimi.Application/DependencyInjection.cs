using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using SatisTalepYonetimi.Application.Behaviors;
using SatisTalepYonetimi.Application.Services;

namespace SatisTalepYonetimi.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(DependencyInjection).Assembly);

            services.AddMediatR(conf =>
            {
                conf.RegisterServicesFromAssemblies(typeof(DependencyInjection).Assembly);
                conf.AddOpenBehavior(typeof(ValidationBehavior<,>));
            });

            services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);

            services.AddScoped<IMaintenanceCheckService, MaintenanceCheckService>();

            return services;
        }
    }
}
