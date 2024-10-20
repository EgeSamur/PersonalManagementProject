using PersonalManagementProject.Shared.Security.JWT;

namespace PersonalManagementProject.Application.Features.Auth.Auth.DTOs;

public class LoggedDto
{
    public int Id { get; set; }
    public IList<string> Roles { get; set; }
    public AccessToken AccessToken { get; set; }
    public IList<string> Permissions { get; set; }
}
