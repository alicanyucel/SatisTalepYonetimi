using SatisTalepYonetimi.Domain.Abstractions;

namespace SatisTalepYonetimi.Domain.Entities
{
    public sealed class PurchaseQuote : Entity
    {
        public Guid SalesRequestId { get; set; }
        public SalesRequest SalesRequest { get; set; } = default!;
        public Guid SupplierId { get; set; }
        public Supplier Supplier { get; set; } = default!;
        public decimal TotalAmount { get; set; }
        public string? Note { get; set; }
        public DateTime QuoteDate { get; set; } = DateTime.UtcNow;
        public bool IsSelected { get; set; }
        public DateTime? ApprovedDate { get; set; }
    }
}
