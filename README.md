[20:14, 04.04.2026] MUSTAFA BEY: Akış

Kullanıcı talep oluşturur
Talep yönetici onayına düşer
Yönetici onay/red işlemi yapar
Onaylanırsa satınalmaya düşer
Satınalma en az 3 teklif toplar
Teklifler yönetim onayına gider
Yönetim bir teklifi seçip onaylar
Bakım yönetiminde periyodik bakım kartları tanımlanır
Bakım zamanı gelince bakım fişi oluşabilir / uyarı maili gönderilir
[20:14, 04.04.2026] Ali Can Yücel: İlgileniyim
[20:14, 04.04.2026] MUSTAFA BEY: Stok Tanımları
Tedarikçi Tanımları olmalı
using System;
using System.Collections.Generic;

namespace ERP.Domain
{
    // ENUMS
    public enum RequestStatus
    {
        PendingManagerApproval,
        Rejected,
        Approved,
        SentToProcurement
    }

    public enum OfferStatus
    {
        Pending,
        Approved,
        Rejected
    }

    public enum MaintenanceStatus
    {
        Planned,
        Completed,
        Delayed
    }

    // USER
    public class AppUser
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }

        public ICollection<Request> Requests { get; set; }
    }

    // REQUEST
    public class Request
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }

        public RequestStatus Status { get; set; }

        public int CreatedById { get; set; }
        public AppUser CreatedBy { get; set; }

        public ICollection<RequestItem> Items { get; set; }
        public ICollection<Offer> Offers { get; set; }
    }

    // REQUEST ITEM
    public class RequestItem
    {
        public int Id { get; set; }

        public int RequestId { get; set; }
        public Request Request { get; set; }

        public string ProductName { get; set; }
        public int Quantity { get; set; }
    }

    // SUPPLIER
    public class Supplier
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public ICollection<Offer> Offers { get; set; }
    }

    // OFFER
    public class Offer
    {
        public int Id { get; set; }

        public int RequestId { get; set; }
        public Request Request { get; set; }

        public int SupplierId { get; set; }
        public Supplier Supplier { get; set; }

        public decimal TotalPrice { get; set; }

        public OfferStatus Status { get; set; }
    }

    // STOCK
    public class Stock
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public DateTime LastUpdated { get; set; }
    }

    // MAINTENANCE PLAN
    public class MaintenancePlan
    {
        public int Id { get; set; }
        public string EquipmentName { get; set; }

        public int PeriodDays { get; set; }
        public DateTime LastMaintenanceDate { get; set; }

        public ICollection<MaintenanceRecord> Records { get; set; }
    }

    // MAINTENANCE RECORD
    public class MaintenanceRecord
    {
        public int Id { get; set; }

        public int MaintenancePlanId { get; set; }
        public MaintenancePlan MaintenancePlan { get; set; }

        public DateTime MaintenanceDate { get; set; }

        public MaintenanceStatus Status { get; set; }
    }

    // WORKFLOW LOG
    public class WorkflowLog
    {
        public int Id { get; set; }

        public string EntityName { get; set; }
        public int EntityId { get; set; }

        public string Action { get; set; }

        public int UserId { get; set; }
        public AppUser User { get; set; }

        public DateTime ActionDate { get; set; }
    }

    // APPROVAL (GENERIC)
    public class Approval
    {
        public int Id { get; set; }

        public string EntityType { get; set; }
        public int EntityId { get; set; }

        public int ApprovedBy { get; set; }
        public bool IsApproved { get; set; }

        public DateTime Date { get; set; }
    }
}
