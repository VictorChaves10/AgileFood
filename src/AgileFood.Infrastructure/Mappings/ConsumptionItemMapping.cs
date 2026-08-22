using AgileFood.Business.Models.Consumptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgileFood.Infrastructure.Mappings;

public class ConsumptionItemMapping : IEntityTypeConfiguration<ConsumptionItem>
{
    public void Configure(EntityTypeBuilder<ConsumptionItem> builder)
    {
        builder.ToTable("ConsumptionItems");

        builder.HasKey(ci => ci.Id);

        builder.Property(ci => ci.ConsumptionId)
            .IsRequired();

        builder.Property(ci => ci.ProductId)
            .IsRequired();

        builder.Property(ci => ci.UnitPrice)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(ci => ci.TotalPrice)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(ci => ci.Quantity)
            .IsRequired();

        builder.Property(ci => ci.ProductName)
            .HasMaxLength(200)
            .IsRequired();
    }
}
