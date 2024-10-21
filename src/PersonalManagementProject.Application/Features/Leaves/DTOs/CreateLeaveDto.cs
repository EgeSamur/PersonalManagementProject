using PersonalManagementProject.Domain.Enums;

namespace PersonalManagementProject.Application.Features.Leaves.DTOs;

public class CreateLeaveDto
{
    public int EmployeeId { get; set; }
    public LeaveType LeaveType { get; set; }
    public string? LeaveTypeDescription { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public LeaveStatus Status { get; set; }
}
