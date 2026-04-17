using SatisTalepYonetimi.Domain.Abstractions;

namespace SatisTalepYonetimi.Domain.Entities
{
    public sealed class SalesRequestItem : Entity
    {
        public Guid SalesRequestId { get; set; }
        public SalesRequest SalesRequest { get; set; } = default!;
        public Guid ProductId { get; set; }
        public Product Product { get; set; } = default!;
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
