namespace PersonalManagementProject.Application.Features.Auth.Permissions.DTOs;

public class AssignPermissionToEmployeeDto
{
    public int EmployeeId { get; set; }  // Tek bir employee
    public List<int> PermissionIds { get; set; }  // Birden fazla permission
}
