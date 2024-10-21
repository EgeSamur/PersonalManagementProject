namespace PersonalManagementProject.Application.Features.Auth.Roles.DTOs;

public class AssignRolesToEmployeeDto
{
    public int EmployeeId { get; set; }
    public List<int> RoleIds { get; set; } = new();
}
