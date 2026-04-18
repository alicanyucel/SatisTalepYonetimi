using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace SatisTalepYonetimi.Infrastructure.Middlewares;

public sealed class TenantMiddleware(RequestDelegate next, ILogger<TenantMiddleware> logger)
{
    private const string TenantHeaderName = "X-Tenant-Id";

    public async Task InvokeAsync(HttpContext context)
    {
        if (context.Request.Headers.TryGetValue(TenantHeaderName, out var tenantId))
        {
            logger.LogInformation("Tenant: {TenantId} ile istek alındı", tenantId.ToString());
            context.Items["TenantId"] = tenantId.ToString();
        }

        await next(context);
    }
}
