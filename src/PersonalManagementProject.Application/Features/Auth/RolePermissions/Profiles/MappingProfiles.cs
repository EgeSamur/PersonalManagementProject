using AutoMapper;
using PersonalManagementProject.Application.Features.Auth.RolePermissions.DTOs;
using PersonalManagementProject.Shared.Utils.Pagination;
using PersonalManagementProject.Shared.Utils.Responses;

namespace PersonalManagementProject.Application.Features.Auth.RolePermissions.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<IPaginate<RolePermissionDto>, PaginatedResponse<RolePermissionDto>>();
    }
}
