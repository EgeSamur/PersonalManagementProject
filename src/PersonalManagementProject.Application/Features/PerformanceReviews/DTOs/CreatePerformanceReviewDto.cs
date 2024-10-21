﻿namespace PersonalManagementProject.Application.Features.PerformanceReviews.DTOs;

public class CreatePerformanceReviewDto
{
    public int EmployeeId { get; set; }
    public int ReviewerId { get; set; }
    public int Score { get; set; } // 0-100
    public DateTime ReviewDate { get; set; }
    public string Comments { get; set; }
}
