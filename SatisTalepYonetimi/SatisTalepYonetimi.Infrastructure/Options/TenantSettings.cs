namespace SatisTalepYonetimi.Infrastructure.Options;

public sealed class TenantSettings
{
    public List<TenantConfig> Tenants { get; set; } = [];
    public string DefaultConnectionString { get; set; } = string.Empty;
}

public sealed class TenantConfig
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string? ConnectionString { get; set; }
}
