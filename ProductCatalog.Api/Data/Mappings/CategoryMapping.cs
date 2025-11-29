using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductCatalog.Api.Models;

namespace ProductCatalog.Api.Data.Mappings;

public sealed class CategoryMapping : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("categories");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
               .HasColumnName("id");

        builder.Property(x => x.Name)
               .HasColumnName("name")
               .HasColumnType("varchar")
               .HasMaxLength(255)
               .IsRequired();

        builder.HasMany(x => x.Products)
               .WithOne(p => p.Category)
               .HasForeignKey("category_id")
               .IsRequired();
    }
}
