using AutoMapper;
using PersonalManagementProject.Application.Features.Employees.DTOs;
using PersonalManagementProject.Shared.Utils.Pagination;
using PersonalManagementProject.Shared.Utils.Responses;

namespace PersonalManagementProject.Application.Features.Employees.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<IPaginate<EmployeeDto>, PaginatedResponse<EmployeeDto>>();
    }
}
