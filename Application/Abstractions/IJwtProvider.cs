using Domain.Entities;
using Domain.Shared;

namespace Application.Abstractions;

public interface IJwtProvider
{
    Task<string> Generate(User user);
    Result<Guid> GetUserId();
}