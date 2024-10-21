using AutoMapper;
using PersonalManagementProject.Application.Features.Departmans.DTOs;
using PersonalManagementProject.Shared.Utils.Pagination;
using PersonalManagementProject.Shared.Utils.Responses;

namespace PersonalManagementProject.Application.Features.Departmans.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<IPaginate<DepartmanDto>, PaginatedResponse<DepartmanDto>>();
    }
}
