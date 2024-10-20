namespace PersonalManagementProject.Application.Features.Auth.Roles.DTOs;

public class CreateRoleDto
{
    public string Name { get; set; }
    public string Key { get; set; }
    public string RoleGroup { get; set; }
    public string? Description { get; set; }
}
