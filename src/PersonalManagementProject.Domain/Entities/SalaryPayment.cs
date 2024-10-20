using PersonalManagementProject.Shared.Domain.Entities;

namespace PersonalManagementProject.Domain.Entities;

public class SalaryPayment : BaseEntity
{
    public int Id { get; set; }
    public int EmployeeId { get; set; }
    public decimal Amount { get; set; }
    public DateTime PaymentDate { get; set; }

    // Navigation property
    public Employee Employee { get; set; }
}



