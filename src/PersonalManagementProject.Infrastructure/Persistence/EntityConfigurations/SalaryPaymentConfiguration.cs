using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonalManagementProject.Domain.Entities;
using PersonalManagementProject.Infrastructure.Persistence.EntityConfigurations.Base;

namespace PersonalManagementProject.Infrastructure.Persistence.EntityConfigurations;

public class SalaryPaymentConfiguration : BaseEntityConfiguration<SalaryPayment>
{
    public override void Configure(EntityTypeBuilder<SalaryPayment> builder)
    {
        base.Configure(builder);
        builder.ToTable("salary_payments");

        builder.Property(s => s.Amount).HasColumnName("amount").IsRequired();
        builder.Property(s => s.PaymentDate).HasColumnName("payment_date").IsRequired();

        // Foreign key for Employee
        builder.HasOne(s => s.Employee)
            .WithMany(e => e.SalaryPayments)
            .HasForeignKey(s => s.EmployeeId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
