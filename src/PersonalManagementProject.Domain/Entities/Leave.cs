using PersonalManagementProject.Domain.Enums;
using PersonalManagementProject.Shared.Domain.Entities;

namespace PersonalManagementProject.Domain.Entities;

public class Leave : BaseEntity
{
    public int Id { get; set; }
    public int EmployeeId { get; set; }
    public LeaveType LeaveType { get; set; }
    public string? LeaveTypeDescription { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public LeaveStatus Status { get; set; }

    // Navigation property
    public Employee Employee { get; set; }
}
