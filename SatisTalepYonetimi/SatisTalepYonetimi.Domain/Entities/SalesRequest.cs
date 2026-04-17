using SatisTalepYonetimi.Domain.Abstractions;

namespace SatisTalepYonetimi.Domain.Entities
{
    public sealed class SalesRequest : Entity
    {
        public string RequestNumber { get; set; } = string.Empty;
        public Guid CustomerId { get; set; }
        public Customer Customer { get; set; } = default!;
        public Guid RequestedByUserId { get; set; }
        public AppUser RequestedByUser { get; set; } = default!;
        public int StatusValue { get; set; } = 1;
        public string? Note { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime RequestDate { get; set; } = DateTime.UtcNow;
        public DateTime? ApprovedDate { get; set; }
        public Guid? ApprovedByUserId { get; set; }
        public AppUser? ApprovedByUser { get; set; }
        public Guid? SelectedQuoteId { get; set; }
        public List<SalesRequestItem> Items { get; set; } = [];
        public List<PurchaseQuote> PurchaseQuotes { get; set; } = [];
    }
}
