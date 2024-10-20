using PersonalManagementProject.Shared.Domain.Entities;

namespace PersonalManagementProject.Domain.Entities.Auth;

public class EmployeeRole : BaseEntity
{
    public int EmployeeId { get; set; }
    public int RoleId { get; set; }

    public Employee Employee { get; set; }
    public Role Role { get; set; }
}


