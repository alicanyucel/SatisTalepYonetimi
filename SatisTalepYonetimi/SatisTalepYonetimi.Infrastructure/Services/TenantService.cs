using Microsoft.AspNetCore.Http;
using SatisTalepYonetimi.Application.Services;

namespace SatisTalepYonetimi.Infrastructure.Services;

public sealed class TenantService(IHttpContextAccessor httpContextAccessor) : ITenantService
{
    private const string TenantHeaderName = "X-Tenant-Id";

    public string? TenantId => httpContextAccessor.HttpContext?.Request.Headers[TenantHeaderName].FirstOrDefault();

    public string? TenantName => TenantId;

    public string? ConnectionString => null; // DB-per-tenant için tenant bazlı connection string döndürülebilir
}
