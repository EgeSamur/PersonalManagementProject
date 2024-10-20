using PersonalManagementProject.Shared.Domain.Entities;

namespace PersonalManagementProject.Domain.Entities.Auth;

public class Role : BaseEntity
{
    public string Name { get; set; }
    public string Key { get; set; }
    public string RoleGroup { get; set; }
    public string? Description { get; set; }

    public ICollection<EmployeeRole> EmployeeRoles { get; set; }
    public ICollection<RolePermission> RolePermissions { get; set; }

}


