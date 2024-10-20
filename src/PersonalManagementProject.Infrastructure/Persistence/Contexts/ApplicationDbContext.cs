using Microsoft.EntityFrameworkCore;
using PersonalManagementProject.Domain.Entities;
using PersonalManagementProject.Domain.Entities.Auth;
using System.Reflection;

namespace PersonalManagementProject.Infrastructure.Persistence.Contexts;

public class ApplicationDbContext : DbContext
{
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Department> Departments { get; set; }
    public DbSet<Leave> Leaves { get; set; }
    public DbSet<PerformanceReview> PerformanceReview { get; set; }
    public DbSet<SalaryPayment> SalaryPayments { get; set; }
    public DbSet<EmployeePermission> EmployeePermissions { get; set; }
    public DbSet<EmployeeRole> EmployeeRoles { get; set; }
    public DbSet<Permission> Permissions { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<RolePermission> RolePermissions { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> dbContextOptions)
        : base(dbContextOptions)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}