using SatisTalepYonetimi.Domain.Abstractions;

namespace SatisTalepYonetimi.Domain.Entities
{
    public sealed class Product : Entity
    {
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal UnitPrice { get; set; }
        public string Unit { get; set; } = string.Empty;
        public int StockQuantity { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
