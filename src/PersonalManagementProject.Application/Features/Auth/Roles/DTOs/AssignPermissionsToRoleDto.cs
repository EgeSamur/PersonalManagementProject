namespace PersonalManagementProject.Application.Features.Auth.Roles.DTOs;

public class AssignPermissionsToRoleDto
{
    public int RoleId { get; set; }
    public List<int> PermissionIds { get; set; } = new();
}