using PersonalManagementProject.Domain.Entities.Auth;
using PersonalManagementProject.Shared.Persistence.Abstraction;

namespace PersonalManagementProject.Application.Abstractions.Repositories;

public interface IRolePermissionRepository : IReadRepository<RolePermission>, IWriteRepository<RolePermission>
{
}
