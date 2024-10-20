using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonalManagementProject.Domain.Entities.Auth;
using PersonalManagementProject.Infrastructure.Persistence.EntityConfigurations.Base;

namespace PersonalManagementProject.Infrastructure.Persistence.EntityConfigurations.Auth;

public class RoleConfiguration : BaseEntityConfiguration<Role>
{
    public override void Configure(EntityTypeBuilder<Role> builder)
    {
        base.Configure(builder);
        builder.ToTable("roles");

        builder.Property(r => r.Name).HasColumnName("name").IsRequired();
        builder.Property(r => r.Key).HasColumnName("key").IsRequired();
        builder.Property(r => r.RoleGroup).HasColumnName("role_group").IsRequired();
        builder.Property(r => r.Description).HasColumnName("description").IsRequired(false);

        // Indexes
        builder.HasIndex(r => r.Key).IsUnique();

        // Relationships
        builder.HasMany(r => r.EmployeeRoles)
            .WithOne(er => er.Role)
            .HasForeignKey(er => er.RoleId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(r => r.RolePermissions)
            .WithOne(rp => rp.Role)
            .HasForeignKey(rp => rp.RoleId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
