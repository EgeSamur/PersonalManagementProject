using PersonalManagementProject.Application.Abstractions.Repositories;
using PersonalManagementProject.Application.Common.Helpers;
using PersonalManagementProject.Application.Features.Auth.Auth.DTOs;
using PersonalManagementProject.Application.Features.Auth.Permissions.DTOs;
using PersonalManagementProject.Application.Features.Auth.RolePermissions.DTOs;
using PersonalManagementProject.Application.Features.Auth.Roles.DTOs;
using PersonalManagementProject.Shared.CrossCuttingConcerns.Exceptions.Types;
using PersonalManagementProject.Shared.Security.Hashing;
using PersonalManagementProject.Shared.Security.JWT;
using PersonalManagementProject.Shared.Utils.Results.Abstract;
using PersonalManagementProject.Shared.Utils.Results.Concrete;
using SnifferApi.Application.Abstractions.Services;

namespace PersonalManagementProject.Application.Features.Auth.Auth;

public class AuthService : IAuthService
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly ITokenHelper _tokenHelper;
    public AuthService(IEmployeeRepository employeeRepository, ITokenHelper tokenHelper)
    {
        _employeeRepository = employeeRepository;
        _tokenHelper = tokenHelper;
    }

    public async Task<IDataResult<MeDto>> Me(int id)
    {
        var me = await _employeeRepository.GetWithProjectionAsync(
           predicate: i => i.Id ==id, // == kullanmalısınız, = değil
           throwExceptionIfNotExists: true,
           notFoundMessage: MessageHelper.NotFound("Me"),
           selector: x => new MeDto
           {
               Id = x.Id,
               FirstName = x.FirstName,
               LastName = x.LastName,
               Email = x.Email,
               PhoneNumber = x.PhoneNumber,
               Position = x.Position,
               Salary = x.Salary,
               HireDate = x.HireDate,
               Roles = x.EmployeeRoles.Select(er => new RoleDto
               {
                   Id = er.Role.Id,
                   Name = er.Role.Name,
                   Key = er.Role.Key,
                   RoleGroup = er.Role.RoleGroup,
                   Description = er.Role.Description,
                   RolePermissions = er.Role.RolePermissions.Select(rp => new RolePermissionDto
                   {
                       Title = rp.Permission.Title,
                       Name = rp.Permission.Name,
                       Key = rp.Permission.Key,
                       Description = rp.Permission.Description
                   }).ToList()
               }).ToList(),
               EmployeePermissions = x.EmployeePermissions.Select(ep => new PermissionDto
               {
                   Id = ep.Permission.Id,
                   Title = ep.Permission.Title,
                   Name = ep.Permission.Name,
                   Key = ep.Permission.Key,
                   Description = ep.Permission.Description
               }).ToList()
           });

        return new SuccessDataResult<MeDto>(me!, MessageHelper.FetchedById("Me"));
    }

    public async Task<IDataResult<LoggedDto>> Login(LoginDto dto)
    {
        var employee = await _employeeRepository.GetWithProjectionAsync(
             predicate: i => i.Email == dto.Email, // == kullanmalısınız, = değil
             throwExceptionIfNotExists: true,
             notFoundMessage: MessageHelper.NotFound("Employee"),
             selector: x => new MeDto
             {
                 Id = x.Id,
                 FirstName = x.FirstName,
                 LastName = x.LastName,
                 Email = x.Email,
                 PhoneNumber = x.PhoneNumber,
                 Position = x.Position,
                 //bunları mecburiyetten ekledim aklıma anlık birşey gelmedi.
                 PasswordHash = x.PasswordHash,
                 PasswordSalt = x.PasswordSalt,
                 Salary = x.Salary,
                 HireDate = x.HireDate,
                 Roles = x.EmployeeRoles.Select(er => new RoleDto
                 {
                     Id = er.Role.Id,
                     Name = er.Role.Name,
                     Key = er.Role.Key,
                     RoleGroup = er.Role.RoleGroup,
                     Description = er.Role.Description,
                     RolePermissions = er.Role.RolePermissions.Select(rp => new RolePermissionDto
                     {
                         Title = rp.Permission.Title,
                         Name = rp.Permission.Name,
                         Key = rp.Permission.Key,
                         Description = rp.Permission.Description
                     }).ToList()
                 }).ToList(),
                 EmployeePermissions = x.EmployeePermissions.Select(ep => new PermissionDto
                 {
                     Id = ep.Permission.Id,
                     Title = ep.Permission.Title,
                     Name = ep.Permission.Name,
                     Key = ep.Permission.Key,
                     Description = ep.Permission.Description
                 }).ToList()
             });
        if (!HashingHelper.VerifyPasswordHash(dto.Password, employee!.PasswordHash, employee.PasswordSalt))
            throw new BusinessException("Giriş bilgileri doğrulanmadı");

        var employeePermissions = employee!.EmployeePermissions.Select(x => x.Key).ToList();
        var rolePermissions = employee.Roles.SelectMany(x => x.RolePermissions).Select(x => x.Key).ToList();
        var allPermissions = employeePermissions.Concat(rolePermissions).Distinct().ToList();
        var roles = employee.Roles.Select(x => x.Name).ToList();
        AccessToken createdAccessToken = _tokenHelper.CreateToken(employee!.Id ,roles.ToArray(),allPermissions.ToArray());
        var result = new LoggedDto
        {
            Id = employee!.Id,
            Roles = roles,
            AccessToken = createdAccessToken,
            Permissions = allPermissions,
        };

        return new SuccessDataResult<LoggedDto>(result!, "Logged.");
    }
}
