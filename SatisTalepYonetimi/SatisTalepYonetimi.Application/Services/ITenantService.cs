namespace SatisTalepYonetimi.Application.Services;

public interface ITenantService
{
    string? TenantId { get; }
    string? TenantName { get; }
    string? ConnectionString { get; }
}
