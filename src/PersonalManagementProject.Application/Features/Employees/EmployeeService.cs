using AutoMapper;
using PersonalManagementProject.Application.Abstractions.Repositories;
using PersonalManagementProject.Application.Common.Helpers;
using PersonalManagementProject.Application.Features.Employees.DTOs;
using PersonalManagementProject.Domain.Entities;
using PersonalManagementProject.Shared.Persistence.Abstraction;
using PersonalManagementProject.Shared.Security.Hashing;
using PersonalManagementProject.Shared.Utils.Results.Abstract;
using PersonalManagementProject.Shared.Utils.Results.Concrete;
using SnifferApi.Application.Abstractions.Services;

namespace PersonalManagementProject.Application.Features.Employees;

public class EmployeeService : IEmployeeService
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public EmployeeService(IEmployeeRepository employeeRepository, IMapper mapper, IUnitOfWork unitOfWork)
    {
        _employeeRepository = employeeRepository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<IResult> CreateAsync(CreateEmployeeDto dto)
    {

        HashingHelper.CreatePasswordHash(dto.Password, out byte[] passwordHash, out byte[] passwordSalt);

        var employee = new Employee()
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt,
            Email = dto.Email,
            PhoneNumber = dto.PhoneNumber,
            Position = dto.Position,
            Salary = dto.Salary,
            HireDate = dto.HireDate,
            IsActive = dto.IsActive,
            DepartmentId = dto.DepartmentId,
        };

        await _employeeRepository.AddAsync(employee);
        await _unitOfWork.SaveChangesAsync();
        return new SuccessResult(MessageHelper.Created("Employe"));
    }

    public async Task<IResult> DeleteAsync(int id)
    {
        var employee = await _employeeRepository.GetAsync(x => x.Id == id,
            throwExceptionIfNotExists: true,
            notFoundMessage: MessageHelper.NotFound("Employee"));
        await _employeeRepository.DeleteAsync(employee!);
        await _unitOfWork.SaveChangesAsync();

        return new SuccessResult(MessageHelper.Deleted("Employee"));
    }

    public async Task<IResult> UpdateAsync(UpdateEmployeeDto dto)
    {
        var employee = await _employeeRepository.GetAsync(i => i.Id == dto.Id,
            throwExceptionIfNotExists: true,
            notFoundMessage: MessageHelper.NotFound("Employee"));


        employee!.FirstName = dto.FirstName;
        employee!.LastName = dto.LastName;
        employee!.Email = dto.Email;
        employee!.PhoneNumber = dto.PhoneNumber;
        employee!.Position = dto.Position;
        employee!.Salary = dto.Salary;
        employee!.HireDate = dto.HireDate;


        await _employeeRepository.UpdateAsync(employee);
        await _unitOfWork.SaveChangesAsync();
        return new SuccessResult(MessageHelper.Updated("Employee"));

    }
}
