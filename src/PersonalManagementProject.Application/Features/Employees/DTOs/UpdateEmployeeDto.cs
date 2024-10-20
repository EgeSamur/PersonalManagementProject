using PersonalManagementProject.Application.Features.Auth.Permissions.DTOs;
using PersonalManagementProject.Application.Features.Auth.Roles.DTOs;

namespace PersonalManagementProject.Application.Features.Employees.DTOs;

public class UpdateEmployeeDto
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Position { get; set; }
    public decimal Salary { get; set; }
    public DateTime HireDate { get; set; }
}


