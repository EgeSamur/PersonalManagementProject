using AutoMapper;
using PersonalManagementProject.Application.Abstractions.Repositories;
using PersonalManagementProject.Application.Common.Helpers;
using PersonalManagementProject.Application.Features.Departmans.DTOs;
using PersonalManagementProject.Domain.Entities;
using PersonalManagementProject.Shared.Persistence.Abstraction;
using PersonalManagementProject.Shared.Utils.Responses;
using PersonalManagementProject.Shared.Utils.Results.Abstract;
using PersonalManagementProject.Shared.Utils.Results.Concrete;
using SnifferApi.Application.Abstractions.Services;

namespace PersonalManagementProject.Application.Features.Departmans;

public class DepartmanService : IDepartmanService
{
    private readonly IDepartmentRepository _departmentRepository;
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public DepartmanService(IDepartmentRepository departmentRepository, IMapper mapper, IUnitOfWork unitOfWork, IEmployeeRepository employeeRepository)
    {
        _departmentRepository = departmentRepository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _employeeRepository = employeeRepository;
    }
    public async Task<IDataResult<PaginatedResponse<DepartmanDto>>> GetAllAsync()
    {
        var depertmans = await _departmentRepository.GetListWithProjectionAsync(
               isAll: true,
               selector: r => new DepartmanDto()
               {
                   Id = r.Id,
                   Name = r.Name,
                   Description = r.Description,
               });

        var result = _mapper.Map<PaginatedResponse<DepartmanDto>>(depertmans);
        return new SuccessDataResult<PaginatedResponse<DepartmanDto>>(result, MessageHelper.Listed("Departments"));
    }
    public async Task<IDataResult<DepartmanDto>> GetByIdAsync(int id)
    {
        var department = await _departmentRepository.GetWithProjectionAsync(
           predicate: i => i.Id == id,
           throwExceptionIfNotExists: true,
           notFoundMessage: MessageHelper.NotFound("Department"),
           selector: r => new DepartmanDto()
           {
               Id = r.Id,
               Name = r.Name,
               Description = r.Description,
           });

        return new SuccessDataResult<DepartmanDto>(department!, MessageHelper.FetchedById("Department"));
    }
    public async Task<IResult> CreateAsync(CreateDepartmanDto dto)
    {
        var departmant = new Department()
        {
            Name = dto.Name,
            Description = dto.Description,
        };

        await _departmentRepository.AddAsync(departmant);
        await _unitOfWork.SaveChangesAsync();
        return new SuccessResult(MessageHelper.Created("Departmant"));
    }
    public async Task<IResult> DeleteAsync(int id)
    {
        var department = await _departmentRepository.GetAsync(x => x.Id == id,
             throwExceptionIfNotExists: true,
             notFoundMessage: MessageHelper.NotFound("Department"));
        await _departmentRepository.DeleteAsync(department!);
        await _unitOfWork.SaveChangesAsync();

        return new SuccessResult(MessageHelper.Deleted("Department"));
    }
    public async Task<IResult> UpdateAsync(UpdateDepartmanDto dto)
    {
        var role = await _departmentRepository.GetAsync(i => i.Id == dto.Id,
              throwExceptionIfNotExists: true,
              notFoundMessage: MessageHelper.NotFound("Department"));

        role!.Name = dto.Name;
        role.Description = dto.Description;

        await _departmentRepository.UpdateAsync(role);
        await _unitOfWork.SaveChangesAsync();
        return new SuccessResult(MessageHelper.Updated("Department"));
    }

    public async Task<IResult> UpdateDepartmanForEmployeeAsync(UpdateDepartmanForEmployeeDto dto)
    {
        var employee = await _employeeRepository.GetAsync(i => i.Id == dto.EmployeeId,
            throwExceptionIfNotExists: true,
            notFoundMessage: MessageHelper.NotFound("Employee"));

        var department = await _departmentRepository.GetAsync(i => i.Id == dto.DepartmanId,
            throwExceptionIfNotExists: true,
            notFoundMessage: MessageHelper.NotFound("Department"));

        employee.DepartmentId = department.Id;

        // Güncellemeleri veritabanına kaydediyoruz
        await _employeeRepository.UpdateAsync(employee);
        await _unitOfWork.SaveChangesAsync();

        return new SuccessResult(MessageHelper.Updated("Employee Department"));
    }
}
