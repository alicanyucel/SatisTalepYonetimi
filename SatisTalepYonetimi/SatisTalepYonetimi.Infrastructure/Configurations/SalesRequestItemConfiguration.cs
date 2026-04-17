using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SatisTalepYonetimi.Domain.Entities;

namespace SatisTalepYonetimi.Infrastructure.Configurations
{
    internal sealed class SalesRequestItemConfiguration : IEntityTypeConfiguration<SalesRequestItem>
    {
        public void Configure(EntityTypeBuilder<SalesRequestItem> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.UnitPrice).HasColumnType("money");
            builder.Property(p => p.TotalPrice).HasColumnType("money");

            builder.HasOne(p => p.Product)
                .WithMany()
                .HasForeignKey(p => p.ProductId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
