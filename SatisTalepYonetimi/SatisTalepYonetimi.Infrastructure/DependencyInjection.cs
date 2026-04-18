using GenericRepository;
using MassTransit;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SatisTalepYonetimi.Application.Services;
using SatisTalepYonetimi.Domain.Entities;
using SatisTalepYonetimi.Infrastructure.Context;
using SatisTalepYonetimi.Infrastructure.Options;
using SatisTalepYonetimi.Infrastructure.Sagas;
using SatisTalepYonetimi.Infrastructure.Services;
using Scrutor;
using System.Reflection;

namespace SatisTalepYonetimi.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("SqlServer"));
            });

            services.AddScoped<IUnitOfWork>(srv => srv.GetRequiredService<ApplicationDbContext>());

            services
                .AddIdentity<AppUser, IdentityRole<Guid>>(cfr =>
                {
                    cfr.Password.RequiredLength = 1;
                    cfr.Password.RequireNonAlphanumeric = false;
                    cfr.Password.RequireUppercase = false;
                    cfr.Password.RequireLowercase = false;
                    cfr.Password.RequireDigit = false;
                    cfr.SignIn.RequireConfirmedEmail = true;
                    cfr.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                    cfr.Lockout.MaxFailedAccessAttempts = 3;
                    cfr.Lockout.AllowedForNewUsers = true;
                })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.Configure<JwtOptions>(configuration.GetSection("Jwt"));
            services.ConfigureOptions<JwtTokenOptionsSetup>();
            services.AddAuthentication()
                .AddJwtBearer();
            services.AddAuthorizationBuilder();

            // Redis Distributed Cache
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = configuration.GetConnectionString("Redis") ?? "localhost:6379";
                options.InstanceName = "SatisTalepYonetimi:";
            });
            services.AddScoped<ICacheService, RedisCacheService>();

            // RabbitMQ + MassTransit
            services.AddMassTransit(busConfigurator =>
            {
                busConfigurator.AddConsumers(Assembly.GetExecutingAssembly());

                busConfigurator.AddSagaStateMachine<SalesRequestSaga, SalesRequestSagaState>()
                    .InMemoryRepository();

                busConfigurator.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(configuration.GetConnectionString("RabbitMQ") ?? "amqp://guest:guest@localhost:5672");
                    cfg.ConfigureEndpoints(context);
                });
            });
            services.AddScoped<IEventPublisher, MassTransitEventPublisher>();

            // Multi-Tenant
            services.AddHttpContextAccessor();
            services.Configure<TenantSettings>(configuration.GetSection("TenantSettings"));
            services.AddScoped<ITenantService, TenantService>();

            // OpenIddict
            services.AddOpenIddict()
                .AddCore(options =>
                {
                    options.UseEntityFrameworkCore()
                        .UseDbContext<ApplicationDbContext>();
                })
                .AddServer(options =>
                {
                    options.SetTokenEndpointUris("connect/token");
                    options.SetAuthorizationEndpointUris("connect/authorize");
                    options.SetIntrospectionEndpointUris("connect/introspect");
                    options.SetRevocationEndpointUris("connect/revoke");

                    options.AllowClientCredentialsFlow();
                    options.AllowAuthorizationCodeFlow();
                    options.AllowRefreshTokenFlow();

                    options.AddDevelopmentEncryptionCertificate()
                        .AddDevelopmentSigningCertificate();

                    options.UseAspNetCore()
                        .EnableTokenEndpointPassthrough()
                        .EnableAuthorizationEndpointPassthrough();
                })
                .AddValidation(options =>
                {
                    options.UseLocalServer();
                    options.UseAspNetCore();
                });

            services.Scan(action =>
            {
                action
                .FromAssemblies(Assembly.GetExecutingAssembly())
                .AddClasses(publicOnly: false)
                .UsingRegistrationStrategy(RegistrationStrategy.Skip)
                .AsMatchingInterface()
                .AsImplementedInterfaces()
                .WithScopedLifetime();
            });

            return services;
        }
    }
}
