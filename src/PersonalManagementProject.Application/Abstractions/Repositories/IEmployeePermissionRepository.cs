using PersonalManagementProject.Domain.Entities.Auth;
using PersonalManagementProject.Shared.Persistence.Abstraction;

namespace PersonalManagementProject.Application.Abstractions.Repositories;
public interface IEmployeePermissionRepository : IReadRepository<EmployeePermission>, IWriteRepository<EmployeePermission>
{
}
