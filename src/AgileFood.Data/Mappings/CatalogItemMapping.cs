using AgileFood.Business.Models.Catalogs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgileFood.Data.Mappings;

public class CatalogItemMapping : IEntityTypeConfiguration<CatalogItem>
{
    public void Configure(EntityTypeBuilder<CatalogItem> builder)
    {
        builder.ToTable("CatalogItems");

        builder.HasKey(ci => ci.Id);

        builder.Property(ci => ci.ProductId)
               .IsRequired();

        builder.HasIndex(ci => ci.ProductId)
               .IsUnique();

        builder.Property(ci => ci.IsAvailable)
               .IsRequired();

        builder.Property(ci => ci.CreatedAt)
               .IsRequired();

        builder.HasOne(ci => ci.Product)
               .WithMany()
               .HasForeignKey(ci => ci.ProductId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}