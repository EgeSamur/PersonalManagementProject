using AutoMapper;
using PersonalManagementProject.Application.Features.PerformanceReviews.DTOs;
using PersonalManagementProject.Shared.Utils.Pagination;
using PersonalManagementProject.Shared.Utils.Responses;

namespace PersonalManagementProject.Application.Features.PerformanceReviews.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<IPaginate<PerformanceReviewDto>, PaginatedResponse<PerformanceReviewDto>>();
    }
}
