using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace PersonalManagementProject.Shared.Security.Encryption;

public static class SecurityKeyHelper
{
    public static SecurityKey CreateSecurityKey(string securityKey) => new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));
}