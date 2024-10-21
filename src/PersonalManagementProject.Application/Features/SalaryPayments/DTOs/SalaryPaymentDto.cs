using PersonalManagementProject.Application.Features.Employees.DTOs;

namespace PersonalManagementProject.Application.Features.SalaryPayments.DTOs;

public class SalaryPaymentDto
{
    public int Id { get; set; }
    public int EmployeeId { get; set; }
    public decimal Amount { get; set; }
    public DateTime PaymentDate { get; set; }
    public EmployeeForSalaryPaymentDto Employee { get; set; }
}