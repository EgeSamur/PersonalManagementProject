using AutoMapper;
using PersonalManagementProject.Application.Features.SalaryPayments.DTOs;
using PersonalManagementProject.Shared.Utils.Pagination;
using PersonalManagementProject.Shared.Utils.Responses;

namespace PersonalManagementProject.Application.Features.SalaryPayments.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<IPaginate<SalaryPaymentDto>, PaginatedResponse<SalaryPaymentDto>>();
    }
}
