using SatisTalepYonetimi.Domain.Abstractions;

namespace SatisTalepYonetimi.Domain.Entities
{
    public sealed class MaintenanceTicket : Entity
    {
        public Guid MaintenanceCardId { get; set; }
        public MaintenanceCard MaintenanceCard { get; set; } = default!;
        public string TicketNumber { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int StatusValue { get; set; } = 1;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? CompletedAt { get; set; }
    }
}
