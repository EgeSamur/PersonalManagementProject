using PersonalManagementProject.Shared.Domain.Entities;

namespace PersonalManagementProject.Domain.Entities;

public class Department : BaseEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }

    // Navigation property - One-to-Many relationship
    public ICollection<Employee> Employees { get; set; }
}
