using PersonalManagementProject.Application.Features.Departmans.DTOs;
using PersonalManagementProject.Application.Features.Leaves.DTOs;
using PersonalManagementProject.Application.Features.PerformanceReviews.DTOs;
using PersonalManagementProject.Application.Features.SalaryPayments.DTOs;

namespace PersonalManagementProject.Application.Features.Employees.DTOs;

public class EmployeeDto
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Position { get; set; }
    public decimal Salary { get; set; }
    public DateTime HireDate { get; set; }
    public DepartmanDto Department { get; set; }
    public ICollection<SalaryPaymentDto> SalaryPayment { get; set; }
    public ICollection<LeaveDto> Leaves { get; set; }
    public ICollection<PerformanceReviewDto> PerformanceReviews { get; set; }
}

