using PersonalManagementProject.Domain.Entities;

namespace PersonalManagementProject.Application.Features.SalaryPayments.DTOs;

public class CreateSalaryPaymentDto
{
    public int EmployeeId { get; set; }
    public decimal Amount { get; set; }
    public DateTime PaymentDate { get; set; }

}
