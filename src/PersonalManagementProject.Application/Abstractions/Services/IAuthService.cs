using PersonalManagementProject.Application.Features.Auth.Auth.DTOs;
using PersonalManagementProject.Shared.Utils.Results.Abstract;

namespace SnifferApi.Application.Abstractions.Services;

public interface IAuthService
{
    Task<IDataResult<MeDto>> Me(int id);
    Task<IDataResult<LoggedDto>> Login(LoginDto dto);

}
