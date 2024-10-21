namespace PersonalManagementProject.Application.Features.SalaryPayments.DTOs;

public class UpdateSalaryPaymentDto
{
    public int Id { get; set; }
    public int EmployeeId { get; set; }
    public decimal Amount { get; set; }
    public DateTime PaymentDate { get; set; }
}
