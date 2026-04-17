using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SatisTalepYonetimi.Domain.Entities;

namespace SatisTalepYonetimi.Infrastructure.Configurations
{
    internal sealed class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Name).HasColumnType("varchar(200)").IsRequired();
            builder.Property(p => p.Code).HasColumnType("varchar(50)").IsRequired();
            builder.Property(p => p.Description).HasColumnType("varchar(500)");
            builder.Property(p => p.UnitPrice).HasColumnType("money");
            builder.Property(p => p.Unit).HasColumnType("varchar(20)");
            builder.HasIndex(p => p.Code).IsUnique();
        }
    }
}
