using PersonalManagementProject.Application.Abstractions.Repositories;
using PersonalManagementProject.Domain.Entities;
using PersonalManagementProject.Infrastructure.Persistence.Contexts;
using PersonalManagementProject.Shared.Persistence.EfCore;

namespace PersonalManagementProject.Infrastructure.Persistence.Repositories;

public class LeaveRepository : RepositoryBase<Leave, ApplicationDbContext>, ILeaveRepository
{
    public LeaveRepository(ApplicationDbContext context) : base(context)
    {
    }
}
