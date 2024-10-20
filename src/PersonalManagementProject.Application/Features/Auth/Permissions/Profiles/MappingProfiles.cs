using AutoMapper;
using PersonalManagementProject.Application.Features.Auth.Permissions.DTOs;
using PersonalManagementProject.Shared.Utils.Pagination;
using PersonalManagementProject.Shared.Utils.Responses;

namespace PersonalManagementProject.Application.Features.Auth.Permissions.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<IPaginate<PermissionDto>, PaginatedResponse<PermissionDto>>();
    }
}
