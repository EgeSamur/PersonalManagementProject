using PersonalManagementProject.Domain.Entities;
using PersonalManagementProject.Shared.Persistence.Abstraction;

namespace PersonalManagementProject.Application.Abstractions.Repositories;

public interface IDepartmentRepository : IReadRepository<Department>, IWriteRepository<Department>
{
}
