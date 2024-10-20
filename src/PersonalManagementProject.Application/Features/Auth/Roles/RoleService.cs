using AutoMapper;
using PersonalManagementProject.Application.Abstractions.Repositories;
using PersonalManagementProject.Application.Common.Helpers;
using PersonalManagementProject.Application.Features.Auth.RolePermissions.DTOs;
using PersonalManagementProject.Application.Features.Auth.Roles.DTOs;
using PersonalManagementProject.Domain.Entities.Auth;
using PersonalManagementProject.Shared.Persistence.Abstraction;
using PersonalManagementProject.Shared.Utils.Responses;
using PersonalManagementProject.Shared.Utils.Results.Abstract;
using PersonalManagementProject.Shared.Utils.Results.Concrete;
using SnifferApi.Application.Abstractions.Services;

namespace PersonalManagementProject.Application.Features.Auth.Roles
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IPermissionRepository _permissionRepository;
        private readonly IRolePermissionRepository _rolePermissionRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IEmployeeRoleRepository _employeeRoleRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public RoleService(IRoleRepository roleRepository, IMapper mapper, IUnitOfWork unitOfWork, IPermissionRepository permissionRepository, IRolePermissionRepository rolePermissionRepository, IEmployeeRepository pmployeermissionRepository, IEmployeeRoleRepository employeeRoleRepository)
        {
            _employeeRepository = pmployeermissionRepository;
            _roleRepository = roleRepository;
            _permissionRepository = permissionRepository;
            _rolePermissionRepository = rolePermissionRepository;
            _employeeRoleRepository = employeeRoleRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<IDataResult<PaginatedResponse<RoleDto>>> GetAllAsync()
        {
            var roles = await _roleRepository.GetListWithProjectionAsync(
                isAll: true,
                selector: r => new RoleDto()
                {
                    Id = r.Id,
                    Name = r.Name,
                    Description = r.Description,
                    Key = r.Key,
                    RoleGroup =r.RoleGroup,
                    RolePermissions = r.RolePermissions.Select(rp => new RolePermissionDto()
                    {
                        Name = rp.Permission.Name,
                        Description = rp.Permission.Description,
                        Key = rp.Permission.Key,
                        Title = rp.Permission.Title

                    }).ToList()
                });

            var result = _mapper.Map<PaginatedResponse<RoleDto>>(roles);
            return new SuccessDataResult<PaginatedResponse<RoleDto>>(result, MessageHelper.Listed("Roles"));
        }
        public async Task<IDataResult<RoleDto>> GetByIdAsync(int id)
        {
            var role = await _roleRepository.GetWithProjectionAsync(
           predicate: i => i.Id == id,
           throwExceptionIfNotExists: true,
           notFoundMessage: MessageHelper.NotFound("Role"),
           selector: r => new RoleDto()
           {
               Id = r.Id,
               Name = r.Name,
               Description = r.Description,
               Key = r.Key,
               RoleGroup = r.RoleGroup,
               RolePermissions = r.RolePermissions.Select(rp => new RolePermissionDto()
               {
                   Name = rp.Permission.Name,
                   Description = rp.Permission.Description,
                   Key = rp.Permission.Key,
                   Title = rp.Permission.Title

               }).ToList()
           });

            return new SuccessDataResult<RoleDto>(role!, MessageHelper.FetchedById("Role"));
        }
        public async Task<IResult> CreateAsync(CreateRoleDto dto)
        {
            var role = new Role()
            {
                Key = dto.Key,
                Name = dto.Name,
                Description = dto.Description,
                RoleGroup = dto.RoleGroup,
            };

            await _roleRepository.AddAsync(role);
            await _unitOfWork.SaveChangesAsync();
            return new SuccessResult(MessageHelper.Created("Role"));
        }
        public async Task<IResult> DeleteAsync(int id)
        {
            var role = await _roleRepository.GetAsync(x => x.Id == id,
                throwExceptionIfNotExists: true,
                notFoundMessage: MessageHelper.NotFound("Role"));
            await _roleRepository.DeleteAsync(role!);
            await _unitOfWork.SaveChangesAsync();

            return new SuccessResult(MessageHelper.Deleted("Role"));
        }
        public async Task<IResult> UpdateAsync(UpdateRoleDto dto)
        {
            var role = await _roleRepository.GetAsync(i => i.Id == dto.Id,
               throwExceptionIfNotExists: true,
               notFoundMessage: MessageHelper.NotFound("Role"));

            role!.Name = dto.Name;
            role.Description = dto.Description;
            role.RoleGroup = dto.RoleGroup;
            role.Key = dto.Key;

            await _roleRepository.UpdateAsync(role);
            await _unitOfWork.SaveChangesAsync();
            return new SuccessResult(MessageHelper.Updated("Role"));
        }
        public async Task<IResult> AssignRoleToEmployeeAsync(AssignRoleToEmployeeDto dto)
        {
            var employee = await _employeeRepository.GetAsync(e => e.Id == dto.EmployeeId, throwExceptionIfNotExists: true, notFoundMessage: MessageHelper.NotFound("Employee"));
            var role = await _roleRepository.GetAsync(r => r.Id == dto.RoleId, throwExceptionIfNotExists: true, notFoundMessage: MessageHelper.NotFound("Role"));

            var employeeRole = new EmployeeRole
            {
                EmployeeId = employee.Id,
                RoleId = role.Id
            };

            await _employeeRoleRepository.AddAsync(employeeRole);
            await _unitOfWork.SaveChangesAsync();

            return new SuccessResult(MessageHelper.Created("Role assigned to employee."));
        }
        public async Task<IResult> AssignPermissionToRoleAsync(AssignPermissionToRoleDto dto)
        {
            var role = await _roleRepository.GetAsync(r => r.Id == dto.RoleId, throwExceptionIfNotExists: true, notFoundMessage: MessageHelper.NotFound("Role"));
            var permission = await _permissionRepository.GetAsync(p => p.Id == dto.PermissionId, throwExceptionIfNotExists: true, notFoundMessage: MessageHelper.NotFound("Permission"));

            var rolePermission = new RolePermission
            {
                RoleId = role.Id,
                PermissionId = permission.Id
            };

            await _rolePermissionRepository.AddAsync(rolePermission);
            await _unitOfWork.SaveChangesAsync();

            return new SuccessResult(MessageHelper.Created("Permission assigned to role."));
        }
    }
}
