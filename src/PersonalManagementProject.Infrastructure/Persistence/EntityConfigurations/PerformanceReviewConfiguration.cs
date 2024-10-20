using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonalManagementProject.Domain.Entities;
using PersonalManagementProject.Infrastructure.Persistence.EntityConfigurations.Base;

namespace PersonalManagementProject.Infrastructure.Persistence.EntityConfigurations;
public class PerformanceReviewConfiguration : BaseEntityConfiguration<PerformanceReview>
{
    public override void Configure(EntityTypeBuilder<PerformanceReview> builder)
    {
        base.Configure(builder);
        builder.ToTable("performance_reviews");

        builder.Property(p => p.Score).HasColumnName("score").IsRequired();
        builder.Property(p => p.ReviewDate).HasColumnName("review_date").IsRequired();
        builder.Property(p => p.Comments).HasColumnName("comments").IsRequired(false);

        // Foreign key for Employee
        builder.HasOne(p => p.Employee)
            .WithMany(e => e.PerformanceReviews)
            .HasForeignKey(p => p.EmployeeId)
            .OnDelete(DeleteBehavior.Cascade);

        // Foreign key for Reviewer (Optional: If reviewer is also an employee)
        builder.HasOne(p => p.Reviewer)
            .WithMany()
            .HasForeignKey(p => p.ReviewerId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
