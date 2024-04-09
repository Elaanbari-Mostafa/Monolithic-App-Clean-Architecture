using Domain.Enums;
using Microsoft.AspNetCore.Authorization;

namespace Infrastructure.Authentification;

public sealed class HasPermissionAttribute : AuthorizeAttribute
{
    public HasPermissionAttribute(Permission permission) : base(permission.ToString())
    {
        
    }
}