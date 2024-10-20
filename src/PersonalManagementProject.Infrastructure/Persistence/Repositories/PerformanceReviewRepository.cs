using PersonalManagementProject.Application.Abstractions.Repositories;
using PersonalManagementProject.Domain.Entities;
using PersonalManagementProject.Infrastructure.Persistence.Contexts;
using PersonalManagementProject.Shared.Persistence.EfCore;

namespace PersonalManagementProject.Infrastructure.Persistence.Repositories;

public class PerformanceReviewRepository : RepositoryBase<PerformanceReview, ApplicationDbContext>, IPerformanceReviewRepository
{
    public PerformanceReviewRepository(ApplicationDbContext context) : base(context)
    {
    }
}
