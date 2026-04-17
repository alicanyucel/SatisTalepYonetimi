using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SatisTalepYonetimi.Domain.Entities;

namespace SatisTalepYonetimi.Infrastructure.Configurations
{
    internal sealed class SupplierConfiguration : IEntityTypeConfiguration<Supplier>
    {
        public void Configure(EntityTypeBuilder<Supplier> builder)
        {
            builder.ToTable("TedarikciTanimlari");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Name).HasColumnType("varchar(200)").IsRequired();
            builder.Property(p => p.ContactPerson).HasColumnType("varchar(200)");
            builder.Property(p => p.Email).HasColumnType("varchar(200)");
            builder.Property(p => p.Phone).HasColumnType("varchar(20)");
            builder.Property(p => p.Address).HasColumnType("varchar(500)");
            builder.Property(p => p.TaxNumber).HasColumnType("varchar(20)");
        }
    }
}
