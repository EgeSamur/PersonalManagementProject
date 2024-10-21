using AutoMapper;
using PersonalManagementProject.Application.Abstractions.Repositories;
using PersonalManagementProject.Application.Common.Helpers;
using PersonalManagementProject.Application.Features.Departmans.DTOs;
using PersonalManagementProject.Application.Features.Employees.DTOs;
using PersonalManagementProject.Application.Features.SalaryPayments.DTOs;
using PersonalManagementProject.Domain.Entities;
using PersonalManagementProject.Shared.Persistence.Abstraction;
using PersonalManagementProject.Shared.Utils.Responses;
using PersonalManagementProject.Shared.Utils.Results.Abstract;
using PersonalManagementProject.Shared.Utils.Results.Concrete;
using SnifferApi.Application.Abstractions.Services;

namespace PersonalManagementProject.Application.Features.SalaryPayments
{
    public class SalaryPaymetService : ISalaryPaymentService
    {
        private readonly ISalaryPaymentRepository _salaryPaymentRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public SalaryPaymetService(ISalaryPaymentRepository salaryPaymentRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _salaryPaymentRepository = salaryPaymentRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<IDataResult<PaginatedResponse<SalaryPaymentDto>>> GetAllAsync()
        {
            var salaryPayment = await _salaryPaymentRepository.GetListWithProjectionAsync(
                     isAll: true,
                     selector: r => new SalaryPaymentDto()
                     {
                         Id = r.Id,
                         EmployeeId = r.EmployeeId,
                         PaymentDate = r.PaymentDate,
                         Employee = new EmployeeForSalaryPaymentDto()
                         {
                             FirstName = r.Employee.FirstName,
                             Email = r.Employee.Email,
                             PhoneNumber = r.Employee.PhoneNumber,
                             Position = r.Employee.Position,
                             HireDate = r.Employee.HireDate,
                             Department = new DepartmanDto()
                             {
                                 Id = r.Employee.Department.Id,
                                 Name = r.Employee.Department.Name,
                                 Description = r.Employee.Department.Description
                             }
                         }
                     });

            var result = _mapper.Map<PaginatedResponse<SalaryPaymentDto>>(salaryPayment);
            return new SuccessDataResult<PaginatedResponse<SalaryPaymentDto>>(result, MessageHelper.Listed("Salary Payment"));
        }

        public async Task<IDataResult<SalaryPaymentDto>> GetByIdAsync(int id)
        {
            var salaryPayment = await _salaryPaymentRepository.GetWithProjectionAsync(
                   predicate: i => i.Id == id,
                   throwExceptionIfNotExists: true,
                   notFoundMessage: MessageHelper.NotFound("Salary Payment"),
                   selector: r => new SalaryPaymentDto()
                   {
                       Id = r.Id,
                       EmployeeId = r.EmployeeId,
                       PaymentDate = r.PaymentDate,
                       Employee = new EmployeeForSalaryPaymentDto()
                       {
                           FirstName = r.Employee.FirstName,
                           Email = r.Employee.Email,
                           PhoneNumber = r.Employee.PhoneNumber,
                           Position = r.Employee.Position,
                           HireDate = r.Employee.HireDate,
                           Department = new DepartmanDto()
                           {
                               Id = r.Employee.Department.Id,
                               Name = r.Employee.Department.Name,
                               Description = r.Employee.Department.Description
                           }
                       }
                   });

            return new SuccessDataResult<SalaryPaymentDto>(salaryPayment!, MessageHelper.FetchedById("Salary Payment"));
        }
        public async Task<IResult> CreateAsync(CreateSalaryPaymentDto dto)
        {
            var salaryPayment = new SalaryPayment()
            {
                EmployeeId = dto.EmployeeId,
                Amount = dto.Amount,
                PaymentDate = dto.PaymentDate,
            };

            await _salaryPaymentRepository.AddAsync(salaryPayment);
            await _unitOfWork.SaveChangesAsync();
            return new SuccessResult(MessageHelper.Created("Salary Payment"));
        }

        public async Task<IResult> DeleteAsync(int id)
        {
            var salaryPayment = await _salaryPaymentRepository.GetAsync(x => x.Id == id,
                 throwExceptionIfNotExists: true,
                 notFoundMessage: MessageHelper.NotFound("Salary Payment"));
            await _salaryPaymentRepository.DeleteAsync(salaryPayment!);
            await _unitOfWork.SaveChangesAsync();

            return new SuccessResult(MessageHelper.Deleted("Salary Payment"));
        }

        public async Task<IResult> UpdateAsync(UpdateSalaryPaymentDto dto)
        {
            var salaryPayment = await _salaryPaymentRepository.GetAsync(i => i.Id == dto.Id,
                  throwExceptionIfNotExists: true,
                  notFoundMessage: MessageHelper.NotFound("Salary Payment"));

            salaryPayment!.EmployeeId = dto.EmployeeId;
            salaryPayment!.Amount = dto.Amount;
            salaryPayment!.PaymentDate = dto.PaymentDate;


            await _salaryPaymentRepository.UpdateAsync(salaryPayment);
            await _unitOfWork.SaveChangesAsync();
            return new SuccessResult(MessageHelper.Updated("Salary Payment"));
        }
    }
}
