﻿namespace Infrastructure.Authentification;

public sealed class PermissionRequirement : IAuthorizationRequirement
{
    public string Permission { get; }
    public PermissionRequirement(string permission)
        => Permission = permission;
}
