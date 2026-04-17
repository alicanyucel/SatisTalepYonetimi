using Ardalis.SmartEnum;

namespace SatisTalepYonetimi.Domain.Enums
{
    public sealed class SalesRequestStatusEnum : SmartEnum<SalesRequestStatusEnum>
    {
        public static readonly SalesRequestStatusEnum Pending = new("Beklemede", 1);
        public static readonly SalesRequestStatusEnum Approved = new("Onaylandı", 2);
        public static readonly SalesRequestStatusEnum Rejected = new("Reddedildi", 3);
        public static readonly SalesRequestStatusEnum Completed = new("Tamamlandı", 4);
        public static readonly SalesRequestStatusEnum Cancelled = new("İptal Edildi", 5);

        private SalesRequestStatusEnum(string name, int value) : base(name, value) { }
    }
}
