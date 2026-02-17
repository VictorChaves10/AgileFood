using AgileFood.Business.Models.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgileFood.Data.Mappings;

public class ProductCategoryMapping : IEntityTypeConfiguration<ProductCategory>
{
    public void Configure(EntityTypeBuilder<ProductCategory> builder)
    {
        builder.ToTable("ProductCategories");

        builder.HasKey(pc => pc.Id);

        builder.Property(pc => pc.Name)
            .IsRequired()
            .HasMaxLength(100);
    }
}

