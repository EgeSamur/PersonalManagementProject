using PersonalManagementProject.Shared.Domain.Entities;

namespace PersonalManagementProject.Domain.Entities;

public class PerformanceReview : BaseEntity
{
    public int Id { get; set; }
    public int EmployeeId { get; set; }
    public int ReviewerId { get; set; }
    public int Score { get; set; } // 0-100
    public DateTime ReviewDate { get; set; }
    public string Comments { get; set; }

    // Navigation property
    public Employee Employee { get; set; }

    // In case the reviewer is also an employee
    public Employee Reviewer { get; set; }
}



