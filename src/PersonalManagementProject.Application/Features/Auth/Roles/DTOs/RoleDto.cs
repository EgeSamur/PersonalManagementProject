using PersonalManagementProject.Application.Features.Auth.RolePermissions.DTOs;

namespace PersonalManagementProject.Application.Features.Auth.Roles.DTOs;

public class RoleDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Key { get; set; }
    public string RoleGroup { get; set; }
    public string? Description { get; set; }
    public ICollection<RolePermissionDto> RolePermissions { get; set; }
}
