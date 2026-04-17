using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SatisTalepYonetimi.Domain.Entities;

namespace SatisTalepYonetimi.Infrastructure.Configurations
{
    internal sealed class SalesRequestConfiguration : IEntityTypeConfiguration<SalesRequest>
    {
        public void Configure(EntityTypeBuilder<SalesRequest> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.RequestNumber).HasColumnType("varchar(50)").IsRequired();
            builder.Property(p => p.Note).HasColumnType("varchar(1000)");
            builder.Property(p => p.TotalAmount).HasColumnType("money");
            builder.HasIndex(p => p.RequestNumber).IsUnique();

            builder.HasOne(p => p.Customer)
                .WithMany()
                .HasForeignKey(p => p.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.RequestedByUser)
                .WithMany()
                .HasForeignKey(p => p.RequestedByUserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(p => p.Items)
                .WithOne(p => p.SalesRequest)
                .HasForeignKey(p => p.SalesRequestId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
