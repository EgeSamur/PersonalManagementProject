using PersonalManagementProject.Shared.CrossCuttingConcerns.Exceptions.ProblemDetails.Models;
using PersonalManagementProject.Shared.CrossCuttingConcerns.Exceptions.Types;
using Microsoft.AspNetCore.Http;

namespace PersonalManagementProject.Shared.CrossCuttingConcerns.Exceptions.ProblemDetails;

public class ValidationProblemDetails : ProblemDetailModel
{
    public IEnumerable<ValidationExceptionModel> Errors { get; init; }

    public ValidationProblemDetails(IEnumerable<ValidationExceptionModel> errors)
    {
        Title = "Validation Error(s)";
        Detail = "One or more validation errors occurred.";
        Errors = errors;
        Status = StatusCodes.Status400BadRequest;
    }
}