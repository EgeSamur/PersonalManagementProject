using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonalManagementProject.Domain.Entities;
using PersonalManagementProject.Infrastructure.Persistence.EntityConfigurations.Base;

namespace PersonalManagementProject.Infrastructure.Persistence.EntityConfigurations;

public class DepartmentConfiguration : BaseEntityConfiguration<Department>
{
    public override void Configure(EntityTypeBuilder<Department> builder)
    {
        base.Configure(builder);
        builder.ToTable("departments");

        builder.Property(d => d.Name).HasColumnName("name").IsRequired();
        builder.Property(d => d.Description).HasColumnName("description").IsRequired(false);

        // Indexes
        builder.HasIndex(d => d.Name).IsUnique();  // Departman adı unique olmalı
    }
}
