using AutoMapper;
using PersonalManagementProject.Application.Features.Leaves.DTOs;
using PersonalManagementProject.Shared.Utils.Pagination;
using PersonalManagementProject.Shared.Utils.Responses;

namespace PersonalManagementProject.Application.Features.Leaves.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<IPaginate<LeaveDto>, PaginatedResponse<LeaveDto>>();
    }
}
