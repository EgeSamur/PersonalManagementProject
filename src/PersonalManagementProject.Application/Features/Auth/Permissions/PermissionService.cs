using AutoMapper;
using PersonalManagementProject.Application.Abstractions.Repositories;
using PersonalManagementProject.Application.Common.Helpers;
using PersonalManagementProject.Application.Features.Auth.Permissions.DTOs;
using PersonalManagementProject.Application.Features.Auth.Roles.DTOs;
using PersonalManagementProject.Domain.Entities.Auth;
using PersonalManagementProject.Shared.Persistence.Abstraction;
using PersonalManagementProject.Shared.Utils.Responses;
using PersonalManagementProject.Shared.Utils.Results.Abstract;
using PersonalManagementProject.Shared.Utils.Results.Concrete;
using SnifferApi.Application.Abstractions.Services;

namespace PersonalManagementProject.Application.Features.Auth.Permissions;

public class PermissionService : IPermissionService
{
    private readonly IPermissionRepository _permissionRepository;
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IEmployeePermissionRepository _employeePermissionRepository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public PermissionService(IPermissionRepository permissionRepository, IEmployeeRepository employeeRepository, IMapper mapper, IUnitOfWork unitOfWork, IEmployeePermissionRepository employeePermissionRepository)
    {
        _permissionRepository = permissionRepository;
        _employeeRepository = employeeRepository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _employeePermissionRepository = employeePermissionRepository;
    }
    public async Task<IDataResult<PaginatedResponse<PermissionDto>>> GetAllAsync()
    {
        var permissions = await _permissionRepository.GetListWithProjectionAsync(
             isAll: true,
             selector: r => new PermissionDto()
             {
                 Id = r.Id,
                 Name = r.Name,
                 Description = r.Description,
                 Key = r.Key,
                 Title = r.Title
             });

        var result = _mapper.Map<PaginatedResponse<PermissionDto>>(permissions);
        return new SuccessDataResult<PaginatedResponse<PermissionDto>>(result, MessageHelper.Listed("Permissions"));
    }
    public async Task<IDataResult<PermissionDto>> GetByIdAsync(int id)
    {
        var permission = await _permissionRepository.GetWithProjectionAsync(
          predicate: i => i.Id == id,
          throwExceptionIfNotExists: true,
          notFoundMessage: MessageHelper.NotFound("Role"),
          selector: r => new PermissionDto()
          {
              Id = r.Id,
              Name = r.Name,
              Description = r.Description,
              Key = r.Key,
              Title = r.Title
          });

        return new SuccessDataResult<PermissionDto>(permission!, MessageHelper.FetchedById("Permission"));
    }
    public async Task<IResult> CreateAsync(CreatePermissionDto dto)
    {
        var permission = new Permission()
        {
            Key = dto.Key,
            Name = dto.Name,
            Description = dto.Description,
            Title = dto.Title,
        };

        await _permissionRepository.AddAsync(permission);
        await _unitOfWork.SaveChangesAsync();
        return new SuccessResult(MessageHelper.Created("Permission"));
    }
    public async Task<IResult> DeleteAsync(int id)
    {
        var permission = await _permissionRepository.GetAsync(x => x.Id == id,
               throwExceptionIfNotExists: true,
               notFoundMessage: MessageHelper.NotFound("Permission"));
        await _permissionRepository.DeleteAsync(permission!);
        await _unitOfWork.SaveChangesAsync();

        return new SuccessResult(MessageHelper.Deleted("Permission"));
    }
    public async Task<IResult> UpdateAsync(UpdatePermissionDto dto)
    {
        var permission = await _permissionRepository.GetAsync(i => i.Id == dto.Id,
              throwExceptionIfNotExists: true,
              notFoundMessage: MessageHelper.NotFound("Permission"));

        permission!.Name = dto.Name;
        permission.Description = dto.Description;
        permission.Title = dto.Title;
        permission.Key = dto.Key;

        await _permissionRepository.UpdateAsync(permission);
        await _unitOfWork.SaveChangesAsync();
        return new SuccessResult(MessageHelper.Updated("Permission"));
    }
    public async Task<IResult> AssignPermissionToEmployeeAsync(AssignPermissionToEmployeeDto dto)
    {
        var employee = await _employeeRepository.GetAsync(e => e.Id == dto.EmployeeId, throwExceptionIfNotExists: true, notFoundMessage: MessageHelper.NotFound("Employee"));
        var permission = await _permissionRepository.GetAsync(r => r.Id == dto.PermissionId, throwExceptionIfNotExists: true, notFoundMessage: MessageHelper.NotFound("Permission"));

        var employeePermission = new EmployeePermission
        {
            EmployeeId = employee!.Id,
            PermissionId = permission!.Id,
        };

        await _employeePermissionRepository.AddAsync(employeePermission);
        await _unitOfWork.SaveChangesAsync();

        return new SuccessResult(MessageHelper.Created("Permisson assigned to employee."));
    }
}
