using AutoMapper;
using PersonalManagementProject.Application.Features.Auth.Roles.DTOs;
using PersonalManagementProject.Shared.Utils.Pagination;
using PersonalManagementProject.Shared.Utils.Responses;

namespace PersonalManagementProject.Application.Features.Auth.Roles.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<IPaginate<RoleDto>, PaginatedResponse<RoleDto>>();
        }
    }
}
