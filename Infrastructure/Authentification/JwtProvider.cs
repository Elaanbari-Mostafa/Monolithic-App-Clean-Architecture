using Application.Abstractions;
using Domain.Entities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static Domain.Exceptions.CustomArgumentNullException;

namespace Infrastructure.Authentification;

public sealed class JwtProvider : IJwtProvider
{
    private readonly JwtOptions _options;
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public JwtProvider(IOptions<JwtOptions> options, IServiceScopeFactory serviceScopeFactory)
        => (_options, _serviceScopeFactory) = (ThrowIfNull(options.Value), serviceScopeFactory);

    public async Task<string> Generate(User user)
    {
        List<Claim> claims = new()
        {
            new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new(JwtRegisteredClaimNames.Email, user.Email.Value),
        };

        IServiceScope scope = _serviceScopeFactory.CreateScope();
        IPermissionService permissionService = scope.ServiceProvider.GetRequiredService<IPermissionService>();
        HashSet<string> permissions = await permissionService.GetPermissionsFromUserIdAsync(user.Id);
        foreach (var permission in permissions)
        {
            claims.Add(new(CustomClaims.Permissions, permission));
        }

        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_options.SecretKey)),
            SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            _options.Issuer,
            _options.Audience,
            claims,
            null,
            DateTime.UtcNow.AddHours(_options.TokenExpirationHours),
            signingCredentials);

        string tokenValue = new JwtSecurityTokenHandler()
            .WriteToken(token);

        return tokenValue;
    }
}