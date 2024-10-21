using AutoMapper;
using PersonalManagementProject.Application.Abstractions.Repositories;
using PersonalManagementProject.Application.Common.Helpers;
using PersonalManagementProject.Application.Features.Leaves.DTOs;
using PersonalManagementProject.Domain.Entities;
using PersonalManagementProject.Shared.Persistence.Abstraction;
using PersonalManagementProject.Shared.Utils.Responses;
using PersonalManagementProject.Shared.Utils.Results.Abstract;
using PersonalManagementProject.Shared.Utils.Results.Concrete;
using SnifferApi.Application.Abstractions.Services;

namespace PersonalManagementProject.Application.Features.Leaves;

public class LeaveService : ILeaveService
{
    private readonly ILeaveRepository _leaveRepository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public LeaveService(ILeaveRepository leaveRepository, IMapper mapper, IUnitOfWork unitOfWork)
    {
        _leaveRepository = leaveRepository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    public async Task<IDataResult<PaginatedResponse<LeaveDto>>> GetAllAsync()
    {
 
    var leaves = await _leaveRepository.GetListWithProjectionAsync(
           isAll: true,
           selector: r => new LeaveDto()
           {
               Id = r.Id,
               EmployeeId = r.EmployeeId,
               LeaveType = r.LeaveType,
               LeaveTypeDescription = r.LeaveTypeDescription,
               StartDate = r.StartDate,
               EndDate = r.EndDate,
               Status = r.Status,
           });

        var result = _mapper.Map<PaginatedResponse<LeaveDto>>(leaves);
        return new SuccessDataResult<PaginatedResponse<LeaveDto>>(result, MessageHelper.Listed("Leaves"));
    }
    public async Task<IDataResult<LeaveDto>> GetByIdAsync(int id)
    {
        var leave = await _leaveRepository.GetWithProjectionAsync(
         predicate: i=> i.Id ==id,
         throwExceptionIfNotExists: true,
         notFoundMessage: MessageHelper.NotFound("Leave"),
         selector: r => new LeaveDto()
         {
             Id = r.Id,
             EmployeeId = r.EmployeeId,
             LeaveType = r.LeaveType,
             LeaveTypeDescription = r.LeaveTypeDescription,
             StartDate = r.StartDate,
             EndDate = r.EndDate,
             Status = r.Status,
         });
        return new SuccessDataResult<LeaveDto>(leave!, MessageHelper.FetchedById("Leave"));
    }
    public async Task<IResult> CreateAsync(CreateLeaveDto dto)
    {
        var salaryPayment = new Leave()
        {
            EmployeeId = dto.EmployeeId,
            LeaveType = dto.LeaveType,
            LeaveTypeDescription = dto.LeaveTypeDescription,
            StartDate = dto.StartDate,
            EndDate = dto.EndDate,
            Status = dto.Status,
        };

        await _leaveRepository.AddAsync(salaryPayment);
        await _unitOfWork.SaveChangesAsync();
        return new SuccessResult(MessageHelper.Created("Leave"));
    }

    public async Task<IResult> DeleteAsync(int id)
    {
        var leave = await _leaveRepository.GetAsync(x => x.Id == id,
             throwExceptionIfNotExists: true,
             notFoundMessage: MessageHelper.NotFound("Leave"));
        await _leaveRepository.DeleteAsync(leave!);
        await _unitOfWork.SaveChangesAsync();

        return new SuccessResult(MessageHelper.Deleted("Leave"));
    }

    public async Task<IResult> UpdateAsync(UpdateLeaveDto dto)
    {
        var leave = await _leaveRepository.GetAsync(i => i.Id == dto.Id,
                  throwExceptionIfNotExists: true,
                  notFoundMessage: MessageHelper.NotFound("Leave"));
        leave!.EmployeeId = dto.EmployeeId;
        leave!.LeaveType = dto.LeaveType;
        leave!.LeaveTypeDescription = dto.LeaveTypeDescription;
        leave!.StartDate = dto.StartDate;
        leave!.EndDate = dto.EndDate;
        leave!.Status = dto.Status;


        await _leaveRepository.UpdateAsync(leave);
        await _unitOfWork.SaveChangesAsync();
        return new SuccessResult(MessageHelper.Updated("Leave"));
    }
}
