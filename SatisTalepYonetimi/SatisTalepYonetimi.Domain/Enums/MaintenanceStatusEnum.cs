using Ardalis.SmartEnum;

namespace SatisTalepYonetimi.Domain.Enums
{
    public sealed class MaintenanceStatusEnum : SmartEnum<MaintenanceStatusEnum>
    {
        public static readonly MaintenanceStatusEnum Open = new("Açık", 1);
        public static readonly MaintenanceStatusEnum InProgress = new("Devam Ediyor", 2);
        public static readonly MaintenanceStatusEnum Completed = new("Tamamlandı", 3);
        public static readonly MaintenanceStatusEnum Cancelled = new("İptal Edildi", 4);

        private MaintenanceStatusEnum(string name, int value) : base(name, value) { }
    }
}
