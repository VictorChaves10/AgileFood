using AgileFood.Business.Models.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgileFood.Infrastructure.Mappings;

public class UserMapping : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(u => u.Id);

        builder.Property(u => u.Name)
               .IsRequired()
               .HasMaxLength(150);

        builder.Property(u => u.Email)
               .IsRequired()
               .HasMaxLength(200);

        builder.HasIndex(u => u.Email)
               .IsUnique();

        builder.Property(u => u.Cpf)
               .IsRequired()
               .HasMaxLength(11);

        builder.HasIndex(u => u.Cpf)
               .IsUnique();

        builder.Property(u => u.EmployeeCode)
               .HasMaxLength(20);

        builder.HasIndex(u => u.EmployeeCode)
               .IsUnique()
               .HasFilter("[EmployeeCode] IS NOT NULL");

        builder.Property(u => u.PasswordHash)
               .IsRequired()
               .HasMaxLength(500);

        builder.Property(u => u.TransactionPinHash)
               .IsRequired()
               .HasMaxLength(500);

        builder.Property(u => u.Role)
               .IsRequired();

        builder.Property(u => u.IsActive)
               .IsRequired();

        builder.Property(u => u.MustChangePassword)
               .IsRequired()
               .HasDefaultValue(false);

        builder.Property(u => u.PasswordResetTokenHash)
               .HasMaxLength(64);

        builder.Property(u => u.FailedPinAttempts)
               .IsRequired()
               .HasDefaultValue(0);

        builder.Property(u => u.CreatedAt)
               .IsRequired();
    }
}
