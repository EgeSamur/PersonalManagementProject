using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonalManagementProject.Domain.Entities.Auth;
using PersonalManagementProject.Infrastructure.Persistence.EntityConfigurations.Base;

namespace PersonalManagementProject.Infrastructure.Persistence.EntityConfigurations.Auth;

public class PermissionConfiguration : BaseEntityConfiguration<Permission>
{
    public override void Configure(EntityTypeBuilder<Permission> builder)
    {
        base.Configure(builder);
        builder.ToTable("permissions");

        builder.Property(p => p.Title).HasColumnName("title").IsRequired();
        builder.Property(p => p.Name).HasColumnName("name").IsRequired();
        builder.Property(p => p.Key).HasColumnName("key").IsRequired();
        builder.Property(p => p.Description).HasColumnName("description").IsRequired(false);

        // Indexes
        builder.HasIndex(p => p.Key).IsUnique();

        // Relationships
        builder.HasMany(p => p.EmployeePermissions)
            .WithOne(ep => ep.Permission)
            .HasForeignKey(ep => ep.PermissionId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(p => p.RolePermissions)
            .WithOne(rp => rp.Permission)
            .HasForeignKey(rp => rp.PermissionId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
