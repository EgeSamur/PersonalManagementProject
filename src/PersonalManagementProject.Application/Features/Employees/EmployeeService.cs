using AutoMapper;
using PersonalManagementProject.Application.Abstractions.Repositories;
using PersonalManagementProject.Application.Common.Helpers;
using PersonalManagementProject.Application.Features.Departmans.DTOs;
using PersonalManagementProject.Application.Features.Employees.DTOs;
using PersonalManagementProject.Application.Features.Leaves.DTOs;
using PersonalManagementProject.Application.Features.PerformanceReviews.DTOs;
using PersonalManagementProject.Application.Features.SalaryPayments.DTOs;
using PersonalManagementProject.Domain.Entities;
using PersonalManagementProject.Domain.Enums;
using PersonalManagementProject.Shared.Persistence.Abstraction;
using PersonalManagementProject.Shared.Security.Hashing;
using PersonalManagementProject.Shared.Utils.Responses;
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

    public async Task<IDataResult<PaginatedResponse<EmployeeDto>>> GetAllAsync()
    {
        var employees = await _employeeRepository.GetListWithProjectionAsync(
                  isAll: true,
                  selector: r => new EmployeeDto()
                  {
                      Id = r.Id,
                      FirstName = r.FirstName,
                      LastName = r.LastName,
                      Email = r.Email,
                      PhoneNumber = r.PhoneNumber,
                      Position = r.Position,
                      Salary = r.Salary,
                      HireDate = r.HireDate,
                      Department = new DepartmanDto()
                      {
                          Id = r.DepartmentId,
                          Name = r.Department.Name,
                          Description = r.Department.Description,
                      },
                      SalaryPayment = r.SalaryPayments.Select(x => new SalaryPaymentDto()
                      {
                          Id = x.Id,
                          EmployeeId = x.EmployeeId,
                          Amount = x.Amount,
                          PaymentDate = x.PaymentDate,
                          // Eğer Employee bilgisi gerekli değilse bu kısmı atlayabilirsin
                      }).ToList(),
                      Leaves = r.Leaves.Select(x => new LeaveDto()
                      {
                          Id = x.Id,
                          EmployeeId = x.EmployeeId,
                          LeaveType = x.LeaveType,
                          LeaveTypeDescription = x.LeaveTypeDescription,
                          StartDate = x.StartDate,
                          EndDate = x.EndDate,
                          Status = x.Status,
                          // Eğer Employee bilgisi gerekli değilse bu kısmı atlayabilirsin
                      }).ToList(),
                      PerformanceReviews = r.PerformanceReviews.Select(x => new PerformanceReviewDto()
                      {

                          Id = x.Id,
                          EmployeeId = x.EmployeeId,
                          ReviewerId = x.ReviewerId,
                          Score = x.Score,
                          ReviewDate = x.ReviewDate,
                          Comments = x.Comments,
                          // Eğer Employee bilgisi gerekli değilse bu kısmı atlayabilirsin
                      }).ToList()

                  });

        var result = _mapper.Map<PaginatedResponse<EmployeeDto>>(employees);
        return new SuccessDataResult<PaginatedResponse<EmployeeDto>>(result, MessageHelper.Listed("Employees"));
    }

    public async Task<IDataResult<EmployeeDto>> GetByIdAsync(int id)
    {
        var employee = await _employeeRepository.GetWithProjectionAsync(
                 predicate: i => i.Id == id,
                 throwExceptionIfNotExists: true,
                 notFoundMessage: MessageHelper.NotFound("Employee"),
                 selector: r => new EmployeeDto()
                 {
                     Id = r.Id,
                     FirstName = r.FirstName,
                     LastName = r.LastName,
                     Email = r.Email,
                     PhoneNumber = r.PhoneNumber,
                     Position = r.Position,
                     Salary = r.Salary,
                     HireDate = r.HireDate,
                     Department = new DepartmanDto()
                     {
                         Id = r.DepartmentId,
                         Name = r.Department.Name,
                         Description = r.Department.Description,
                     },
                     SalaryPayment = r.SalaryPayments.Select(x => new SalaryPaymentDto()
                     {
                         Id = x.Id,
                         EmployeeId = x.EmployeeId,
                         Amount = x.Amount,
                         PaymentDate = x.PaymentDate,
                         // Eğer Employee bilgisi gerekli değilse bu kısmı atlayabilirsin
                     }).ToList(),
                     Leaves = r.Leaves.Select(x => new LeaveDto()
                     {
                         Id = x.Id,
                         EmployeeId = x.EmployeeId,
                         LeaveType = x.LeaveType,
                         LeaveTypeDescription = x.LeaveTypeDescription,
                         StartDate = x.StartDate,
                         EndDate = x.EndDate,
                         Status = x.Status,
                         // Eğer Employee bilgisi gerekli değilse bu kısmı atlayabilirsin
                     }).ToList(),
                     PerformanceReviews = r.PerformanceReviews.Select(x => new PerformanceReviewDto()
                     {

                         Id = x.Id,
                         EmployeeId = x.EmployeeId,
                         ReviewerId = x.ReviewerId,
                         Score = x.Score,
                         ReviewDate = x.ReviewDate,
                         Comments = x.Comments,
                         // Eğer Employee bilgisi gerekli değilse bu kısmı atlayabilirsin
                     }).ToList()

                 });
        return new SuccessDataResult<EmployeeDto>(employee!, MessageHelper.FetchedById("Employees"));
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
        return new SuccessResult(MessageHelper.Created("Employee"));
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
