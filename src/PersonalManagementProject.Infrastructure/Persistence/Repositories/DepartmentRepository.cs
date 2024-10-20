using PersonalManagementProject.Application.Abstractions.Repositories;
using PersonalManagementProject.Domain.Entities;
using PersonalManagementProject.Infrastructure.Persistence.Contexts;
using PersonalManagementProject.Shared.Persistence.EfCore;

namespace PersonalManagementProject.Infrastructure.Persistence.Repositories;

public class DepartmentRepository : RepositoryBase<Department, ApplicationDbContext>, IDepartmentRepository
{
    public DepartmentRepository(ApplicationDbContext context) : base(context)
    {
    }
}
