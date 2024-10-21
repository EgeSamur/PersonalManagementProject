using PersonalManagementProject.Application.Features.Departmans.DTOs;

namespace PersonalManagementProject.Application.Features.Employees.DTOs;

public class EmployeeForSalaryPaymentDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Position { get; set; }
    public DateTime HireDate { get; set; }
    public DepartmanDto Department { get; set; }
}

