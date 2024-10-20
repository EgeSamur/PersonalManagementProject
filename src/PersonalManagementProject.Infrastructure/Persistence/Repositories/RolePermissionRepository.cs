using PersonalManagementProject.Application.Abstractions.Repositories;
using PersonalManagementProject.Domain.Entities.Auth;
using PersonalManagementProject.Infrastructure.Persistence.Contexts;
using PersonalManagementProject.Shared.Persistence.EfCore;

namespace PersonalManagementProject.Infrastructure.Persistence.Repositories;

public class RolePermissionRepository : RepositoryBase<RolePermission, ApplicationDbContext>, IRolePermissionRepository
{
    public RolePermissionRepository(ApplicationDbContext context) : base(context)
    {
    }
}
