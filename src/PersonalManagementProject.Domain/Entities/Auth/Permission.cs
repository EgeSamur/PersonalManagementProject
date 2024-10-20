using PersonalManagementProject.Shared.Domain.Entities;

namespace PersonalManagementProject.Domain.Entities.Auth;

public class Permission : BaseEntity
{
    public string Title { get; set; }
    public string Name { get; set; }
    public string Key { get; set; }
    public string? Description { get; set; }

    public ICollection<EmployeePermission> EmployeePermissions { get; set; }
    public ICollection<RolePermission> RolePermissions { get; set; }
}


