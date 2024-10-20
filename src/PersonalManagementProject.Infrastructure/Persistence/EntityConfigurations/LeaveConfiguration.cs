using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonalManagementProject.Domain.Entities;
using PersonalManagementProject.Infrastructure.Persistence.EntityConfigurations.Base;

namespace PersonalManagementProject.Infrastructure.Persistence.EntityConfigurations;

public class LeaveConfiguration : BaseEntityConfiguration<Leave>
{
    public override void Configure(EntityTypeBuilder<Leave> builder)
    {
        base.Configure(builder);
        builder.ToTable("leaves");

        builder.Property(l => l.StartDate).HasColumnName("start_date").IsRequired();
        builder.Property(l => l.EndDate).HasColumnName("end_date").IsRequired();
        builder.Property(l => l.LeaveType).HasColumnName("leave_type").IsRequired();
        builder.Property(l => l.Status).HasColumnName("status").IsRequired();

        // Foreign key for Employee
        builder.HasOne(l => l.Employee)
            .WithMany(e => e.Leaves)
            .HasForeignKey(l => l.EmployeeId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
