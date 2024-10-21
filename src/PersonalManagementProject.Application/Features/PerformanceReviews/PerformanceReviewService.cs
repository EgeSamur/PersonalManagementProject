using AutoMapper;
using PersonalManagementProject.Application.Abstractions.Repositories;
using PersonalManagementProject.Application.Common.Helpers;
using PersonalManagementProject.Application.Features.PerformanceReviews.DTOs;
using PersonalManagementProject.Domain.Entities;
using PersonalManagementProject.Shared.Persistence.Abstraction;
using PersonalManagementProject.Shared.Utils.Responses;
using PersonalManagementProject.Shared.Utils.Results.Abstract;
using PersonalManagementProject.Shared.Utils.Results.Concrete;
using SnifferApi.Application.Abstractions.Services;

namespace PersonalManagementProject.Application.Features.PerformanceReviews;

public class PerformanceReviewService : IPerformanceReviewService
{
    private readonly IPerformanceReviewRepository _performanceReviewRepository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public PerformanceReviewService(IPerformanceReviewRepository performanceReviewRepository, IMapper mapper, IUnitOfWork unitOfWork)
    {
        _performanceReviewRepository = performanceReviewRepository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    public async Task<IDataResult<PaginatedResponse<PerformanceReviewDto>>> GetAllAsync()
    {
        var performanceViews = await _performanceReviewRepository.GetListWithProjectionAsync(
              isAll: true,
              selector: r => new PerformanceReviewDto()
              {

                  Id = r.Id,
                  EmployeeId = r.EmployeeId,
                  ReviewerId = r.ReviewerId,
                  Score = r.Score,
                  ReviewDate = r.ReviewDate,
                  Comments = r.Comments,
              });

        var result = _mapper.Map<PaginatedResponse<PerformanceReviewDto>>(performanceViews);
        return new SuccessDataResult<PaginatedResponse<PerformanceReviewDto>>(result, MessageHelper.Listed("Performance Review"));
    }

    public async Task<IDataResult<PerformanceReviewDto>> GetByIdAsync(int id)
    {
        var performanceView = await _performanceReviewRepository.GetWithProjectionAsync(
             predicate: i => i.Id == id,
             throwExceptionIfNotExists: true,
             notFoundMessage: MessageHelper.NotFound("Performance View"),
             selector: r => new PerformanceReviewDto()
             {

                 Id = r.Id,
                 EmployeeId = r.EmployeeId,
                 ReviewerId = r.ReviewerId,
                 Score = r.Score,
                 ReviewDate = r.ReviewDate,
                 Comments = r.Comments,

             });
        return new SuccessDataResult<PerformanceReviewDto>(performanceView!, MessageHelper.FetchedById("Department"));
    }
    public async Task<IResult> CreateAsync(CreatePerformanceReviewDto dto)
    {
        var performanceReview = new PerformanceReview()
        {
            EmployeeId = dto.EmployeeId,
            ReviewerId = dto.ReviewerId,
            Score = dto.Score,
            ReviewDate = dto.ReviewDate,
            Comments = dto.Comments,
        };

        await _performanceReviewRepository.AddAsync(performanceReview);
        await _unitOfWork.SaveChangesAsync();
        return new SuccessResult(MessageHelper.Created("Performance Review"));
    }
    public async Task<IResult> DeleteAsync(int id)
    {
        var department = await _performanceReviewRepository.GetAsync(x => x.Id == id,
          throwExceptionIfNotExists: true,
          notFoundMessage: MessageHelper.NotFound("Performance Review"));
        await _performanceReviewRepository.DeleteAsync(department!);
        await _unitOfWork.SaveChangesAsync();

        return new SuccessResult(MessageHelper.Deleted("Performance Review"));
    }
    public async Task<IResult> UpdateAsync(UpdatePerformanceReviewDto dto)
    {
        var performanceReview = await _performanceReviewRepository.GetAsync(i => i.Id == dto.Id,
             throwExceptionIfNotExists: true,
             notFoundMessage: MessageHelper.NotFound("Performance Review"));

        performanceReview!.EmployeeId = dto.EmployeeId;
        performanceReview!.ReviewerId = dto.ReviewerId;
        performanceReview!.Score = dto.Score;
        performanceReview!.ReviewDate = dto.ReviewDate;
        performanceReview!.Comments = dto.Comments;
        await _performanceReviewRepository.UpdateAsync(performanceReview);
        await _unitOfWork.SaveChangesAsync();
        return new SuccessResult(MessageHelper.Updated("Performance Review"));
    }
}
