using DefaultCorsPolicyNugetPackage;
using Hangfire;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using SatisTalepYonetimi.Application;
using SatisTalepYonetimi.Infrastructure;
using SatisTalepYonetimi.WebAPI.Middlewares;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day)
    .Enrich.FromLogContext()
    .MinimumLevel.Information()
    .CreateLogger();

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Host.UseSerilog();

    builder.Services.AddMemoryCache();

    builder.Services.AddOpenTelemetry()
        .ConfigureResource(resource => resource.AddService("SatisTalepYonetimi"))
        .WithTracing(tracing =>
        {
            tracing
                .AddAspNetCoreInstrumentation()
                .AddHttpClientInstrumentation()
                .AddSqlClientInstrumentation()
                .AddOtlpExporter();
        })
        .WithMetrics(metrics =>
        {
            metrics
                .AddAspNetCoreInstrumentation()
                .AddHttpClientInstrumentation()
                .AddPrometheusExporter();
        });

    builder.Services.AddHangfire(config =>
        config.UseSqlServerStorage(builder.Configuration.GetConnectionString("SqlServer")));
    builder.Services.AddHangfireServer();

    builder.Services.AddDefaultCors();
    builder.Services.AddApplication();
    builder.Services.AddInfrastructure(builder.Configuration);

    builder.Services.AddExceptionHandler<ExceptionHandler>();
    builder.Services.AddProblemDetails();

    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(setup =>
    {
        var jwtSecuritySheme = new OpenApiSecurityScheme
        {
            BearerFormat = "JWT",
            Name = "JWT Authentication",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.Http,
            Scheme = JwtBearerDefaults.AuthenticationScheme,
            Description = "Put **_ONLY_** yourt JWT Bearer token on textbox below!",

            Reference = new OpenApiReference
            {
                Id = JwtBearerDefaults.AuthenticationScheme,
                Type = ReferenceType.SecurityScheme
            }
        };

        setup.AddSecurityDefinition(jwtSecuritySheme.Reference.Id, jwtSecuritySheme);

        setup.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                        { jwtSecuritySheme, Array.Empty<string>() }
                    });
    });

    var app = builder.Build();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseSerilogRequestLogging();

    app.UseHttpsRedirection();

    app.UseCors();

    app.UseExceptionHandler();

    app.UseOpenTelemetryPrometheusScrapingEndpoint();

    app.UseHangfireDashboard("/hangfire");

    RecurringJob.AddOrUpdate<SatisTalepYonetimi.Application.Services.IMaintenanceCheckService>(
        "maintenance-check",
        service => service.CheckAndCreateTicketsAsync(),
        Cron.Daily);

    app.MapControllers();

    ExtensionsMiddleware.CreateFirstUser(app);

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Uygulama başlatılırken hata oluştu");
}
finally
{
    Log.CloseAndFlush();
}
