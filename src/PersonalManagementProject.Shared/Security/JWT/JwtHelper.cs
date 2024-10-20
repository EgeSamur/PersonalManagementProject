using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using PersonalManagementProject.Shared.Security.Encryption;
using PersonalManagementProject.Shared.Security.Extensions;

namespace PersonalManagementProject.Shared.Security.JWT;

public class JwtHelper : ITokenHelper
{
    public IConfiguration Configuration { get; }
    private readonly TokenOptions _tokenOptions;
    private DateTime _accessTokenExpiration;

    public JwtHelper(IConfiguration configuration)
    {
        Configuration = configuration;
        const string configurationSection = "TokenOptions";
        _tokenOptions =
            Configuration.GetSection(configurationSection).Get<TokenOptions>()
            ?? throw new NullReferenceException($"\"{configurationSection}\" section cannot found in configuration.");
    }


    public AccessToken CreateToken(int id, string[] roles, string[] permissions)
    {
        _accessTokenExpiration = DateTime.Now.AddHours(12);
        SecurityKey securityKey = SecurityKeyHelper.CreateSecurityKey(_tokenOptions.SecurityKey);
        SigningCredentials signingCredentials = SigningCredentialsHelper.CreateSigningCredentials(securityKey);
        JwtSecurityToken jwt = CreateJwtSecurityToken(_tokenOptions, id, roles, permissions, signingCredentials);
        JwtSecurityTokenHandler jwtSecurityTokenHandler = new();
        string? token = jwtSecurityTokenHandler.WriteToken(jwt);

        return new AccessToken { Token = token, Expiration = _accessTokenExpiration };
    }


    public JwtSecurityToken CreateJwtSecurityToken(
        TokenOptions tokenOptions,
        int id,
        string[] roles, string[] permissions,
        SigningCredentials signingCredentials
    )
    {
        JwtSecurityToken jwt =
            new(
                tokenOptions.Issuer,
                tokenOptions.Audience,
                expires: _accessTokenExpiration,
                notBefore: DateTime.Now,
                claims: SetClaims(id, roles, permissions),
                signingCredentials: signingCredentials
            );
        return jwt;
    }

    private IEnumerable<Claim> SetClaims(int id, string[] roles, string[] permissions)
    {
        List<Claim> claims = new();
        claims.AddNameIdentifier(id.ToString());
        claims.AddRoles(roles);
        claims.AddPermissions(permissions);
        return claims;
    }
}