using AgileFood.Business.Models.Consumptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgileFood.Data.Mappings;

public class ConsumptionMapping : IEntityTypeConfiguration<Consumption>
{
    public void Configure(EntityTypeBuilder<Consumption> builder)
    {
        builder.ToTable("Consumptions");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.UserId)
               .IsRequired();

        builder.Property(c => c.ProductId)
               .IsRequired();

        builder.Property(c => c.Quantity)
               .IsRequired();

        builder.Property(c => c.UnitPrice)
               .IsRequired()
               .HasColumnType("decimal(18,2)");

        builder.Property(c => c.TotalPrice)
               .IsRequired()
               .HasColumnType("decimal(18,2)");

        builder.Property(c => c.ConsumedAt)
               .IsRequired();

        builder.Property(c => c.ReferenceMonth)
               .IsRequired();

        builder.Property(c => c.ReferenceYear)
               .IsRequired();

        builder.HasIndex(c => new { c.UserId, c.ReferenceYear, c.ReferenceMonth })
               .HasDatabaseName("IX_Consumptions_User_Period");

        builder.HasOne(c => c.User)
               .WithMany()
               .HasForeignKey(c => c.UserId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(c => c.Product)
               .WithMany()
               .HasForeignKey(c => c.ProductId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}