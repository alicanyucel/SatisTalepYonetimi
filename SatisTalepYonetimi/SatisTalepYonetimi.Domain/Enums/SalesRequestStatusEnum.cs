using Ardalis.SmartEnum;

namespace SatisTalepYonetimi.Domain.Enums
{
    public sealed class SalesRequestStatusEnum : SmartEnum<SalesRequestStatusEnum>
    {
        public static readonly SalesRequestStatusEnum Pending = new("Beklemede", 1);
        public static readonly SalesRequestStatusEnum ManagerApproval = new("Yönetici Onayında", 2);
        public static readonly SalesRequestStatusEnum ManagerApproved = new("Yönetici Onayladı", 3);
        public static readonly SalesRequestStatusEnum Rejected = new("Reddedildi", 4);
        public static readonly SalesRequestStatusEnum ProcurementPending = new("Satınalma Sürecinde", 5);
        public static readonly SalesRequestStatusEnum QuotesCollected = new("Teklifler Toplandı", 6);
        public static readonly SalesRequestStatusEnum ManagementApproval = new("Yönetim Onayında", 7);
        public static readonly SalesRequestStatusEnum QuoteApproved = new("Teklif Onaylandı", 8);
        public static readonly SalesRequestStatusEnum Completed = new("Tamamlandı", 9);
        public static readonly SalesRequestStatusEnum Cancelled = new("İptal Edildi", 10);

        private SalesRequestStatusEnum(string name, int value) : base(name, value) { }
    }
}
