using System.Security.Claims;

namespace PersonalManagementProject.Shared.Security.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static List<string>? Claims(this ClaimsPrincipal claimsPrincipal, string claimType)
    {
        var result = claimsPrincipal?.FindAll(claimType)?.Select(x => x.Value).ToList();
        return result;
    }

    public static List<string>? ClaimRoles(this ClaimsPrincipal claimsPrincipal) => claimsPrincipal?.Claims(ClaimTypes.Role);

    public static int? GetUserId(this ClaimsPrincipal claimsPrincipal)
    {
        var id = claimsPrincipal?.Claims(ClaimTypes.NameIdentifier)?.FirstOrDefault();
        return id != null ? int.Parse(id) : null;
    }
}