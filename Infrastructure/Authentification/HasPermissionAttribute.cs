namespace Infrastructure.Authentification;

public sealed class HasPermissionAttribute : AuthorizeAttribute
{
    public HasPermissionAttribute(EPermission permission) : base(permission.ToString())
    {
        
    }
}