using PersonalManagementProject.Shared.Domain.Entities;

namespace PersonalManagementProject.Domain.Entities.Auth;

public class RolePermission : BaseEntity
{
    public int RoleId { get; set; }
    public int PermissionId { get; set; }

    public Role Role { get; set; }
    public Permission Permission { get; set; }
}


