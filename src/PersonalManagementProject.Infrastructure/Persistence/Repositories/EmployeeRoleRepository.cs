using PersonalManagementProject.Application.Abstractions.Repositories;
using PersonalManagementProject.Domain.Entities.Auth;
using PersonalManagementProject.Infrastructure.Persistence.Contexts;
using PersonalManagementProject.Shared.Persistence.EfCore;

namespace PersonalManagementProject.Infrastructure.Persistence.Repositories;

public class EmployeeRoleRepository : RepositoryBase<EmployeeRole, ApplicationDbContext>, IEmployeeRoleRepository
{
    public EmployeeRoleRepository(ApplicationDbContext context) : base(context)
    {
    }
}
