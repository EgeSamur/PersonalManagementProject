namespace PersonalManagementProject.Application.Features.Employees.DTOs;

public class CreateEmployeeDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Position { get; set; }
    public decimal Salary { get; set; }
    public DateTime HireDate { get; set; }
    public bool IsActive { get; set; } = true;
    public int DepartmentId { get; set; }
}

