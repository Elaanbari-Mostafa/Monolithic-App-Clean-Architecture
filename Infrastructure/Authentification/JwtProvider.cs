using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.Authentification;

public sealed class JwtProvider : IJwtProvider
{
    private readonly JwtOptions _options;
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private const string Authorization = "Authorization";
    private const string JwtAuthorizationBearerSplit = " ";

    public JwtProvider(IOptions<JwtOptions> options, IServiceScopeFactory serviceScopeFactory)
        => (_options, _serviceScopeFactory) = (ThrowIfNull(options.Value), ThrowIfNull(serviceScopeFactory));

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

    public Result<Guid> GetUserId()
    {
        IServiceScope scope = _serviceScopeFactory.CreateScope();
        IHttpContextAccessor? httpContextAccessor = scope.ServiceProvider.GetService<IHttpContextAccessor>();

        Result<string> token = httpContextAccessor?.HttpContext?.Request.Headers[Authorization]
            .FirstOrDefault()?
            .Split(JwtAuthorizationBearerSplit)
            .Last();
        if (token.IsFailure)
        {
            return Result.Failure<Guid>(DomainErrors.Jwt.TokenNotFoundInTheRequestHeaders);
        }

        var handler = new JwtSecurityTokenHandler();
        var jsonToken = handler.ReadToken(token.Value) as JwtSecurityToken;

        Result<Claim> userIdClaim = jsonToken?.Claims.FirstOrDefault(claim => claim.Type == JwtRegisteredClaimNames.Sub);
        if (userIdClaim.IsFailure)
        {
            return Result.Failure<Guid>(DomainErrors.Jwt.TokenNotFoundOrInvalide(JwtRegisteredClaimNames.Sub));
        }

        return Guid.Parse(userIdClaim.Value.Value);

    }
}