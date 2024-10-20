using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonalManagementProject.Domain.Entities.Auth;
using PersonalManagementProject.Infrastructure.Persistence.EntityConfigurations.Base;

namespace PersonalManagementProject.Infrastructure.Persistence.EntityConfigurations.Auth;

public class EmployeeRoleConfiguration : BaseEntityConfiguration<EmployeeRole>
{
    public override void Configure(EntityTypeBuilder<EmployeeRole> builder)
    {
        base.Configure(builder);
        builder.ToTable("employee_roles");

        builder.Property(er => er.EmployeeId).HasColumnName("employee_id").IsRequired();
        builder.Property(er => er.RoleId).HasColumnName("role_id").IsRequired();

        // Foreign Key Constraints
        builder.HasOne(er => er.Employee)
            .WithMany(e => e.EmployeeRoles)
            .HasForeignKey(er => er.EmployeeId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(er => er.Role)
            .WithMany(r => r.EmployeeRoles)
            .HasForeignKey(er => er.RoleId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
