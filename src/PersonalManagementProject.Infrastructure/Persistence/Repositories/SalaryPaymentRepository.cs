using PersonalManagementProject.Application.Abstractions.Repositories;
using PersonalManagementProject.Domain.Entities;
using PersonalManagementProject.Infrastructure.Persistence.Contexts;
using PersonalManagementProject.Shared.Persistence.EfCore;

namespace PersonalManagementProject.Infrastructure.Persistence.Repositories;

public class SalaryPaymentRepository : RepositoryBase<SalaryPayment, ApplicationDbContext>, ISalaryPaymentRepository
{
    public SalaryPaymentRepository(ApplicationDbContext context) : base(context)
    {
    }
}
