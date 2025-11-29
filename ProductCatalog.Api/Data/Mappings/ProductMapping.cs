using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductCatalog.Api.Models;

namespace ProductCatalog.Api.Data.Mappings;

public sealed class ProductMapping : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("products");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
               .HasColumnName("id");

        builder.Property(x => x.Name)
               .HasColumnName("name")
               .HasColumnType("varchar")
               .HasMaxLength(255)
               .IsRequired();

        builder.Property(x => x.Description)
               .HasColumnName("description")
               .HasColumnType("varchar")
               .HasMaxLength(255)
               .IsRequired(false);

        builder.Property(x => x.Price)
               .HasColumnName("price");
    }
}
