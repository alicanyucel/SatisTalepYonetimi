using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SatisTalepYonetimi.Domain.Entities;

namespace SatisTalepYonetimi.Infrastructure.Configurations
{
    internal sealed class PurchaseQuoteConfiguration : IEntityTypeConfiguration<PurchaseQuote>
    {
        public void Configure(EntityTypeBuilder<PurchaseQuote> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.TotalAmount).HasColumnType("money");
            builder.Property(p => p.Note).HasColumnType("varchar(1000)");

            builder.HasOne(p => p.SalesRequest)
                .WithMany(p => p.PurchaseQuotes)
                .HasForeignKey(p => p.SalesRequestId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(p => p.Supplier)
                .WithMany()
                .HasForeignKey(p => p.SupplierId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
