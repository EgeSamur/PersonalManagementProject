using PersonalManagementProject.Shared.Domain.Entities;

namespace PersonalManagementProject.Domain.Entities.Auth;

public class EmployeePermission : BaseEntity
{
    public int EmployeeId { get; set; }
    public int PermissionId { get; set; }

    public Employee Employee { get; set; }
    public Permission Permission { get; set; }
}


