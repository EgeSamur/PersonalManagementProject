using PersonalManagementProject.Shared.CrossCuttingConcerns.Exceptions.ProblemDetails.Models;
using Microsoft.AspNetCore.Http;

namespace PersonalManagementProject.Shared.CrossCuttingConcerns.Exceptions.ProblemDetails;

public class NotFoundProblemDetails : ProblemDetailModel
{
    public NotFoundProblemDetails(string detail)
    {
        Title = "Not Found";
        Detail = detail;
        Status = StatusCodes.Status404NotFound;
    }
}