using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonalManagementProject.Domain.Entities;
using PersonalManagementProject.Infrastructure.Persistence.EntityConfigurations.Base;

namespace PersonalManagementProject.Infrastructure.Persistence.EntityConfigurations;

public class EmployeeConfiguration : BaseEntityConfiguration<Employee>
{
    public override void Configure(EntityTypeBuilder<Employee> builder)
    {
        base.Configure(builder);
        builder.ToTable("employees");

        builder.Property(e => e.FirstName).HasColumnName("first_name").IsRequired();
        builder.Property(e => e.LastName).HasColumnName("last_name").IsRequired();
        builder.Property(e => e.Email).HasColumnName("email").IsRequired();
        builder.Property(e => e.PhoneNumber).HasColumnName("phone_number").IsRequired();
        builder.Property(e => e.Position).HasColumnName("position").IsRequired();
        builder.Property(e => e.Salary).HasColumnName("salary").IsRequired();
        builder.Property(e => e.HireDate).HasColumnName("hire_date").IsRequired();
        builder.Property(e => e.IsActive).HasColumnName("is_active").IsRequired();

        // Şifre alanını ekleyelim
        builder.Property(e => e.PasswordSalt).HasColumnName("password_salt").IsRequired();
        builder.Property(e => e.PasswordHash).HasColumnName("password_hash").IsRequired();

        // Indexes
        builder.HasIndex(e => e.Email).IsUnique();  // Email alanı unique olacak
    }
}
