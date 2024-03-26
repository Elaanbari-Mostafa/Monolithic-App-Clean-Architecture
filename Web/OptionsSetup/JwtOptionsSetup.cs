using Infrastructure.Authentification;
using Microsoft.Extensions.Options;
using static Domain.Exceptions.CustomArgumentNullException;

namespace Web.OptionsSetup;

public sealed class JwtOptionsSetup : IConfigureOptions<JwtOptions>
{
    private readonly IConfiguration _configuration;
    private const string SectionName = "Jwt";

    public JwtOptionsSetup(IConfiguration configuration) => _configuration = ThrowIfNull(configuration);

    public void Configure(JwtOptions options) => ThrowIfNull(_configuration.GetSection(SectionName)).Bind(options);
}