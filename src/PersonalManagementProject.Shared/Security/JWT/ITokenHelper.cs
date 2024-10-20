namespace PersonalManagementProject.Shared.Security.JWT;

public interface ITokenHelper
{
    AccessToken CreateToken(int id, string[] roles, string[] permissions);
}