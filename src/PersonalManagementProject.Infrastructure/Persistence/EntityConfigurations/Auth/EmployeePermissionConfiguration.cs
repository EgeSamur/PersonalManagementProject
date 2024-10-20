using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonalManagementProject.Domain.Entities.Auth;
using PersonalManagementProject.Infrastructure.Persistence.EntityConfigurations.Base;

namespace PersonalManagementProject.Infrastructure.Persistence.EntityConfigurations.Auth;

public class EmployeePermissionConfiguration : BaseEntityConfiguration<EmployeePermission>
{
    public override void Configure(EntityTypeBuilder<EmployeePermission> builder)
    {
        base.Configure(builder);
        builder.ToTable("employee_permissions");

        builder.Property(ep => ep.EmployeeId).HasColumnName("employee_id").IsRequired();
        builder.Property(ep => ep.PermissionId).HasColumnName("permission_id").IsRequired();

        // Foreign Key Constraints
        builder.HasOne(ep => ep.Employee)
            .WithMany(e => e.EmployeePermissions)
            .HasForeignKey(ep => ep.EmployeeId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(ep => ep.Permission)
            .WithMany(p => p.EmployeePermissions)
            .HasForeignKey(ep => ep.PermissionId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
