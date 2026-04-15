    using AgileFood.Business.Models.Stock;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgileFood.Data.Mappings;

public class StockMovementMapping : IEntityTypeConfiguration<StockMovement>
{
    public void Configure(EntityTypeBuilder<StockMovement> builder)
    {
        builder.ToTable("StockMovements");

        builder.HasKey(sm => sm.Id);

        builder.Property(sm => sm.Type)
               .IsRequired();

        builder.Property(sm => sm.Origin)
               .IsRequired();

        builder.Property(sm => sm.Quantity)
               .IsRequired();

        builder.Property(sm => sm.Reason)
               .HasMaxLength(300);

        builder.Property(sm => sm.ConsumptionId);

        builder.Property(sm => sm.Date)
               .IsRequired();

        builder.HasOne(sm => sm.Consumption)
               .WithMany()
               .HasForeignKey(sm => sm.ConsumptionId)
               .OnDelete(DeleteBehavior.SetNull);
    }
}
