using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SatisTalepYonetimi.Domain.Entities;

namespace SatisTalepYonetimi.Infrastructure.Configurations
{
    internal sealed class MaintenanceCardConfiguration : IEntityTypeConfiguration<MaintenanceCard>
    {
        public void Configure(EntityTypeBuilder<MaintenanceCard> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Name).HasColumnType("varchar(200)").IsRequired();
            builder.Property(p => p.Description).HasColumnType("varchar(500)");
            builder.Property(p => p.AssignedEmail).HasColumnType("varchar(200)");

            builder.HasOne(p => p.Product)
                .WithMany()
                .HasForeignKey(p => p.ProductId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
