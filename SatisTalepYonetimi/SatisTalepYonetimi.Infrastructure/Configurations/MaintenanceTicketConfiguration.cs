using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SatisTalepYonetimi.Domain.Entities;

namespace SatisTalepYonetimi.Infrastructure.Configurations
{
    internal sealed class MaintenanceTicketConfiguration : IEntityTypeConfiguration<MaintenanceTicket>
    {
        public void Configure(EntityTypeBuilder<MaintenanceTicket> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.TicketNumber).HasColumnType("varchar(50)").IsRequired();
            builder.Property(p => p.Description).HasColumnType("varchar(1000)");
            builder.HasIndex(p => p.TicketNumber).IsUnique();

            builder.HasOne(p => p.MaintenanceCard)
                .WithMany()
                .HasForeignKey(p => p.MaintenanceCardId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
